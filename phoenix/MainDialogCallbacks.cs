namespace phoenix
{
    using Properties;
    using System.Threading.Tasks;

    // All of these callbacks are called on UI thread
    public partial class MainDialog
    {
        private int m_MqttRetryMinutes = 2;
        private void OnProcessStop()
        {
            ResetWatchButtonLabel();
            Logger.Warn(string.Format("Process stopped ({0}).", m_ProcessRunner.ProcessPath));
        }

        private void OnProcessStart()
        {
            ResetWatchButtonLabel();
            Logger.Info(string.Format("Process started ({0}).", m_ProcessRunner.ProcessPath));
        }

        private void OnMonitorStop()
        {
            Logger.Warn("Monitor stopped.");
            ResetWatchButtonLabel();
            SendCrashEmail();
        }

        private void OnMonitorStart()
        {
            Logger.Info("Monitor started.");
            ResetWatchButtonLabel();
        }

        private void OnMqttConnectionOpen()
        {
            Logger.Info("MQTT connection established.");
            ResetMqttConnectionLabel();
        }

        private void OnMqttConnectionClose()
        {
            ResetMqttConnectionLabel();
            Logger.Warn(string.Format("MQTT connection closed, retrying in {0} minutes."
                , m_MqttRetryMinutes));

            Task.Delay(new System.TimeSpan(0, m_MqttRetryMinutes, 0)).ContinueWith(fn => {
                Logger.Info("MQTT attempting to reconnect.");
                m_RemoteManager.Connect(mqtt_server_address.Text, Resources.MqttTopic);
            });
        }

        private void OnMqttMessage(string message, string topic)
        {
            Logger.Info(string.Format("MQTT message received: ({0}) from ({1}).", message, Resources.MqttTopic));

            if (topic != Resources.MqttTopic)
                return;

            if (message == "echo") {
                string echo = string.Format("{{ \"name\":\"{0}\", \"public_key\":\"{1}\" }}",
                    RsyncClient.MachineIdentity,
                    RsyncClient.PublicKey.Trim('\n'));

                m_RemoteManager.Publish(echo, string.Format("{0}/machines", Resources.MqttTopic));
            }
        }
    }
}
