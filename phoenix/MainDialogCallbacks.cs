namespace phoenix
{
    using Properties;

    // All of these callbacks are called on UI thread
    public partial class MainDialog
    {
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
            Logger.Warn("MQTT connection closed.");
            ResetMqttConnectionLabel();
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
