namespace phoenix
{
    using System;
    using System.Text;
    using uPLibrary.Networking.M2Mqtt;
    using uPLibrary.Networking.M2Mqtt.Messages;

    /// <summary>
    /// Remote manager handles logic necessary to receive commands over
    /// MQTT and execute tasks locally.
    /// </summary>
    class RemoteManager : IDisposable
    {
        /// <summary>MQTT client instance</summary>
        MqttClient m_client;
        /// <summary>MQTT channel to listen on</summary>
        string m_channel;
        /// <summary>MQTT server to connect to</summary>
        string m_address;

        /// <summary>Event broadcaster on MQTT connection closed.</summary>
        public Action OnConnectionClosed;
        /// <summary>Event broadcaster on MQTT connection opened.</summary>
        public Action OnConnectionOpened;
        /// <summary>Event broadcaster on MQTT message. Message on Topic are passed in order.</summary>
        public Action<string, string> OnMessage;

        /// <summary>
        /// Attempts to connect to an MQTT server
        /// </summary>
        /// <param name="address">MQTT network accessible address</param>
        /// <param name="channel">MQTT channel path</param>
        public void Connect(string address, string channel)
        {
            if (String.IsNullOrWhiteSpace(address) ||
                String.IsNullOrWhiteSpace(channel))
            {
                Logger.RemoteManager.ErrorFormat("Connect failed, invalid arguments.");
                ConnectionClosed();
                return;
            }

            if (m_client != null &&
                m_client.IsConnected &&
                m_address == address &&
                m_channel == channel)
            {
                Logger.RemoteManager.WarnFormat("Connection is already established.");
                return;
            }

            try
            {
                if (Connected) m_client.Disconnect();
                m_client = new MqttClient(address);
            }
            catch (Exception ex)
            {
                Logger.RemoteManager.ErrorFormat("Connect failed: {0}", ex.Message);
                ConnectionClosed();
                return;
            }

            m_client.MqttMsgPublishReceived += MqttMessageReceived;
            m_client.ConnectionClosed += (s, e) =>
            {
                Logger.RemoteManager.Warn("MQTT connection closed.");
                ConnectionClosed();
            };

            try
            {
                m_client.Connect(RsyncClient.MachineIdentity);

                if (m_client.IsConnected)
                {
                    m_client.Subscribe(
                        new string[] { channel },
                        new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
                    m_channel = channel;

                    Logger.RemoteManager.InfoFormat(
                        "Established an MQTT connection to {0} and subscribed to {1}.",
                        address, channel);

                    m_address = address;
                    ConnectionOpened();
                }
                else
                {
                    Logger.RemoteManager.Error("MQTT connection is not established after Connect!");
                    ConnectionClosed();
                }
            }
            catch (Exception ex)
            {
                Logger.RemoteManager.ErrorFormat("Connect failed: {0}", ex.Message);
                ConnectionClosed();
                return;
            }
        }

        /// <summary>
        /// Subscribe to an MQTT channel. No-op if no connection
        /// </summary>
        /// <param name="channel">MQTT channel</param>
        public void Subscribe(string channel)
        {
            if (m_client.IsConnected)
            {
                m_client.Subscribe(
                    new string[] { channel },
                    new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

                Logger.RemoteManager.InfoFormat("Subscribed to {0}.", channel);
            }
            else
            {
                Logger.RemoteManager.Error("Cannot subscribe if connection is not established");
            }
        }

        /// <summary>
        /// Publish to an MQTT channel. No-op if no connection
        /// </summary>
        /// <param name="message">message to be published</param>
        /// <param name="channel">channel to be published to</param>
        public void Publish(string message, string channel = "")
        {
            if (m_client == null || !m_client.IsConnected || message == string.Empty)
                return;

            if (String.IsNullOrWhiteSpace(channel))
                channel = m_channel;

            if (!String.IsNullOrWhiteSpace(channel))
                m_client.Publish(
                    channel,
                    Encoding.UTF8.GetBytes(message),
                    MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,
                    false); // retain flag
        }

        /// <summary>
        /// Boolean flag, answers true if MQTT connection is established
        /// </summary>
        public bool Connected
        {
            get { return m_client != null && m_client.IsConnected; }
        }

        //! @cond

        void ConnectionOpened()
        {
            if (OnConnectionOpened != null)
                OnConnectionOpened();
        }

        void ConnectionClosed()
        {
            if (OnConnectionClosed != null)
                OnConnectionClosed();
        }

        void MqttMessageReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string msg = Encoding.UTF8.GetString(e.Message);

            Logger.RemoteManager.InfoFormat("MQTT message received: ({0}) from ({1}).",
                msg, e.Topic);

            if (OnMessage != null)
                OnMessage(msg, e.Topic);
        }

        #region IDisposable Support
        private bool m_Disposed = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!m_Disposed)
            {
                if (disposing)
                {
                    if (Connected)
                        m_client.Disconnect();
                }

                m_Disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

        //! @endcond
    }
}
