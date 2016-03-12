namespace phoenix.Metrics
{
    using System.Collections.Generic;
    using OpenHardwareMonitor.Hardware;

    /// <summary>
    /// Interface for all collectors we will be using in Phoenix
    /// Collectors are: CPU, GPU, RAM, and HDD for now
    /// </summary>
    public abstract class ICollector
    {
        protected IHardware     m_Hardware;
        protected List<ISensor> m_Sensors;
        private double          m_CurrentSample = 0.0d;
        private bool            m_SetupCalled   = false;

        /// <summary>
        /// Call this with an "opened" Computer instance to
        /// setup this collector instance. After Setup, calls
        /// to Update and GetCurrentSample are valid.
        /// </summary>
        /// <param name="computer">opened Computer instance</param>
        public void Setup(Computer computer)
        {
            m_SetupCalled = false;

            if (computer == null)
            {
                Logger.Collector.Error("Setup is called with empty computer.");
                return;
            }

            m_Sensors = new List<ISensor>();
            CollectSensors(computer);
            m_CurrentSample = 0.0f;

            if (m_Hardware != null)
            {
                m_Hardware.SensorAdded += (sensor) =>
                {
                    Logger.Collector.Info("A sensor is added.");

                    if (m_Sensors != null)
                    {
                        if (sensor.SensorType == SensorType.Load)
                            m_Sensors.Add(sensor);
                    }
                };

                m_Hardware.SensorRemoved += (sensor) =>
                {
                    Logger.Collector.Info("A sensor is removed.");

                    if (m_Sensors != null)
                    {
                        if (sensor.SensorType == SensorType.Load)
                            m_Sensors.Remove(sensor);
                    }
                };
            }
            else
            {
                Logger.Collector.Warn("Hardware is empty.");
                return;
            }

            m_SetupCalled = true;
        }

        /// <summary>
        /// Subclasses override this to acquire appropriate sensors
        /// </summary>
        /// <param name="computer">Opened computer instance</param>
        protected abstract void CollectSensors(Computer computer);

        /// <summary>
        /// Call this to update the sample obtained via GetCurrentSample
        /// </summary>
        public void Update()
        {
            if (!m_SetupCalled || m_Sensors == null || m_Hardware == null || m_Sensors.Count == 0)
                return;

            m_Hardware.Update();

            foreach (IHardware subhardware in m_Hardware.SubHardware)
                subhardware.Update();

            double sum = 0d;
            foreach (var sensor in m_Sensors)
                if (sensor.Value.HasValue)
                    sum += sensor.Value.Value;

            m_CurrentSample = (sum / m_Sensors.Count) / 100d;
        }

        /// <summary>
        /// Gets the current sample
        /// </summary>
        public double GetCurrentSample()
        {
            return m_CurrentSample;
        }
    }
}
