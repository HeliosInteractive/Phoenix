namespace phoenix.Metrics
{
    using System;
    using System.Linq;
    using OpenHardwareMonitor.Hardware;

    public class Manager : IDisposable
    {
        private Computer    m_Computer;
        private ICollector  m_CpuCollector  = new CpuCollector();
        private ICollector  m_GpuCollector  = new GpuCollector();
        private ICollector  m_RamCollector  = new RamCollector();
        private double[]    m_CpuSamples    = new double[m_NumSamples];
        private double[]    m_GpuSamples    = new double[m_NumSamples];
        private double[]    m_RamSamples    = new double[m_NumSamples];
        private const int   m_NumSamples    = 100;

        /// <summary>
        /// Number of samples which is being collected for each Collector
        /// </summary>
        public int      NumSamples { get { return m_NumSamples; } }

        /// <summary>
        /// Samples Collected for CPU load
        /// </summary>
        public double[] CpuSamples { get { return m_CpuSamples; } }

        /// <summary>
        /// Samples Collected for GPU load
        /// </summary>
        public double[] GpuSamples { get { return m_GpuSamples; } }

        /// <summary>
        /// Samples Collected for RAM load
        /// </summary>
        public double[] RamSamples { get { return m_RamSamples; } }

        /// <summary>
        /// Initializes all Collectors with an opened instance of Computer
        /// </summary>
        public Manager()
        {
            m_Computer = new Computer
            {
                CPUEnabled = true,
                GPUEnabled = true,
                RAMEnabled = true,
            };

            m_Computer.Open();

            m_CpuCollector.Setup(m_Computer);
            m_GpuCollector.Setup(m_Computer);
            m_RamCollector.Setup(m_Computer);

            m_CpuSamples = Enumerable.Repeat(0d, NumSamples).ToArray();
            m_GpuSamples = Enumerable.Repeat(0d, NumSamples).ToArray();
            m_RamSamples = Enumerable.Repeat(0d, NumSamples).ToArray();
        }

        /// <summary>
        /// Updates all collectors and collects their new samples
        /// </summary>
        public void Update()
        {
            for (int index = 1; index < NumSamples; ++index)
            {
                CpuSamples[index - 1] = CpuSamples[index];
                GpuSamples[index - 1] = GpuSamples[index];
                RamSamples[index - 1] = RamSamples[index];
            }

            m_CpuCollector.Update();
            m_GpuCollector.Update();
            m_RamCollector.Update();

            int last_index = NumSamples - 1;
            CpuSamples[last_index] = m_CpuCollector.GetCurrentSample();
            GpuSamples[last_index] = m_GpuCollector.GetCurrentSample();
            RamSamples[last_index] = m_RamCollector.GetCurrentSample();
        }

        //! @cond
        #region IDisposable Support
        private bool m_Disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!m_Disposed)
            {
                if (disposing)
                {
                    if (m_Computer != null)
                        m_Computer.Close();
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
