namespace phoenix
{
    using Properties;
    using System.Threading.Tasks;

    // All of these callbacks are called on UI thread
    public partial class MainDialog
    {
        private int m_MqttRetryMinutes = 2;
        private void OnProcessStop(ProcessRunner.ExecType type)
        {
            if (m_PhoenixReady && type == ProcessRunner.ExecType.CRASHED)
                SendCrashEmail();

            ResetWatchButtonLabel();
            Logger.MainDialog.WarnFormat("Process stopped ({0}).", m_ProcessRunner.ProcessPath);
        }

        private void OnProcessStart(ProcessRunner.ExecType type)
        {
            ResetWatchButtonLabel();
            m_AppSettings.Store("Internal", "CachedName", m_ProcessRunner.CachedTitle);
            Logger.MainDialog.InfoFormat("Process started ({0}).", m_ProcessRunner.ProcessPath);
        }

        private void OnMqttConnectionOpen()
        {
            ResetMqttConnectionLabel();
            Logger.MainDialog.Info("MQTT connection established.");
        }

        private void OnMqttConnectionClose()
        {
            ResetMqttConnectionLabel();
            Logger.MainDialog.WarnFormat("MQTT connection closed, retrying in {0} minutes."
                , m_MqttRetryMinutes);

            Task.Delay(new System.TimeSpan(0, m_MqttRetryMinutes, 0)).ContinueWith(fn => {
                Logger.MainDialog.Info("MQTT attempting to reconnect.");
                m_RemoteManager.Connect(mqtt_server_address.Text, Resources.MqttTopic);
            });
        }

        private void OnMqttMessage(string message, string topic)
        {
            if (topic != Resources.MqttTopic)
                return;

            if (message == "echo") {
                string echo = string.Format("{{ \"name\":\"{0}\", \"public_key\":\"{1}\" }}",
                    RsyncClient.MachineIdentity,
                    RsyncClient.PublicKey.Trim('\n'));

                m_RemoteManager.Publish(echo, string.Format("{0}/{1}",
                    Resources.MqttTopic,
                    message));
            }
            else if (message == "ping") {
                string ping = string.Format("{{ \"name\":\"{0}\", \"cpu\":{1}, \"mem\":{2} }}",
                    RsyncClient.MachineIdentity,
                    m_ProcessRunner.LastCpuUsage,
                    m_ProcessRunner.LastMemUsage);

                m_RemoteManager.Publish(ping, string.Format("{0}/{1}",
                    Resources.MqttTopic,
                    message));
            }
        }
    }
}
