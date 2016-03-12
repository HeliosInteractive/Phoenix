namespace phoenix
{
    using Properties;
    using System.Threading.Tasks;

    // All of these callbacks are called on UI thread
    public partial class MainDialog
    {
        private int m_MqttRetryMinutes = 2;
        private string m_MachineChannel = string.Format("{0}/{1}",
                Resources.MqttTopic,
                RsyncClient.MachineIdentity);
        private void OnProcessStop(ProcessRunner.ExecType type)
        {
            if (m_PhoenixReady && type == ProcessRunner.ExecType.Crashed)
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
            m_RemoteManager.Subscribe(m_MachineChannel);
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
            if (topic == Resources.MqttTopic)
            {
                if (message == "echo")
                {
                    string echo = string.Format("{{ \"name\":\"{0}\", \"public_key\":\"{1}\" }}",
                        RsyncClient.MachineIdentity,
                        RsyncClient.PublicKey.Trim('\n'));

                    m_RemoteManager.Publish(echo, string.Format("{0}/{1}",
                        Resources.MqttTopic,
                        message));
                }
                else if (message == "ping")
                {
                    double last_mem_usage = 0d;
                    double last_cpu_usage = 0d;

                    if (m_MetricsManager.NumSamples > 0 && m_MetricsManager.RamSamples != null)
                        last_mem_usage = m_MetricsManager.RamSamples[m_MetricsManager.RamSamples.Length - 1];

                    if (m_MetricsManager.NumSamples > 0 && m_MetricsManager.CpuSamples != null)
                        last_cpu_usage = m_MetricsManager.CpuSamples[m_MetricsManager.CpuSamples.Length - 1];

                    string ping = string.Format("{{ \"name\":\"{0}\", \"cpu\":{1}, \"mem\":{2}, \"monitoring\":{3} }}",
                        RsyncClient.MachineIdentity,
                        last_cpu_usage,
                        last_mem_usage,
                        m_ProcessRunner.Monitoring.ToString().ToLower());

                    m_RemoteManager.Publish(ping, string.Format("{0}/{1}",
                        Resources.MqttTopic,
                        message));
                }
            }
            else if (topic == m_MachineChannel)
            {
                if (message == "stop") {
                    m_ProcessRunner.Stop(ProcessRunner.ExecType.Normal);
                } else if (message == "start") {
                    m_ProcessRunner.Start(ProcessRunner.ExecType.Normal);
                } else if (message == "update") {
                    OnPullUpdateClick(null, null);
                } else if (message == "upgrade") {
                    m_UpdateManager.Check();
                } else if (message == "report") {
                    SendCrashEmail();
                }
            }
        }
    }
}
