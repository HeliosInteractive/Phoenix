namespace phoenix.Metrics
{
    using OpenHardwareMonitor.Hardware;

    /// <summary>
    /// GPU metric collector
    /// </summary>
    public class GpuCollector : ICollector
    {
        protected override void CollectSensors(Computer computer)
        {
            foreach (IHardware hardwareItem in computer.Hardware)
            {
                if (hardwareItem.HardwareType == HardwareType.GpuAti ||
                    hardwareItem.HardwareType == HardwareType.GpuNvidia)
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
