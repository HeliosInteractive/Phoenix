namespace phoenix.Metrics
{
    using OpenHardwareMonitor.Hardware;

    /// <summary>
    /// CPU metric collector
    /// </summary>
    public class CpuCollector : ICollector
    {
        protected override void CollectSensors(Computer computer)
        {
            foreach (IHardware hardwareItem in computer.Hardware)
            {
                if (hardwareItem.HardwareType == HardwareType.CPU)
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
