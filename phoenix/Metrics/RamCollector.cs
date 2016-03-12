namespace phoenix.Metrics
{
    using OpenHardwareMonitor.Hardware;

    /// <summary>
    /// RAM metric collector
    /// </summary>
    public class RamCollector : ICollector
    {
        protected override void CollectSensors(Computer computer)
        {
            foreach (IHardware hardwareItem in computer.Hardware)
            {
                if (hardwareItem.HardwareType == HardwareType.RAM)
                {
                    m_Hardware = hardwareItem;

                    foreach (ISensor sensor in hardwareItem.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Load)
                        {
                            m_Sensors.Add(sensor);
                        }
                    }
                }
            }
        }
    }
}
