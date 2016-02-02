namespace phoenix
{
    using System.Diagnostics;

    class Logger
    {
        public static void Info(string msg)
        {
            Trace.TraceInformation(msg);
        }
        public static void Warn(string msg)
        {
            Trace.TraceWarning(msg);
        }
        public static void Error(string msg)
        {
            Trace.TraceError(msg);
        }
    }
}
