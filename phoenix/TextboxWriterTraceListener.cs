namespace phoenix
{
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;

    class TextboxWriterTraceListener : TraceListener
    {
        private TextBoxBase logbox;

        public TextboxWriterTraceListener(TextBoxBase box, string name)
        {
            base.Name = name;
            logbox = box;
        }

        public override void Write(string message)
        {
            if (logbox == null || logbox.IsDisposed)
                return;

            try
            {
                logbox.BeginInvoke((MethodInvoker)(() =>
                {
                // double check pattern, I hate C#
                if (logbox == null || logbox.IsDisposed)
                        return;

                    logbox.AppendText(message + Environment.NewLine);
                }));
            }
            catch
            {
                /* I really cannot do anything here! */
                Trace.TraceError(string.Format("Text box listener invoked without a valid textbox. msg: {0}", message));
            }
        }

        public override void WriteLine(string message)
        {
            Write(message);
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            Write(message);
        }
    }
}
