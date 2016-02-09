namespace phoenix
{
    using System;
    using System.Windows.Forms;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainDialog());
            System.Diagnostics.Trace.Flush();
        }

        public static string Directory
        {
            get
            {
                return System.IO.Path.GetDirectoryName(
                    System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
        }
    }
}
