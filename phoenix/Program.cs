namespace phoenix
{
    using System;
    using System.Windows.Forms;

    static class Program
    {
        //! @cond
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainDialog());
            System.Diagnostics.Trace.Flush();
        }
        //! @endcond

        /// <summary>
        /// returns the current directory where Phoenix's executable is
        /// physically located. (absolute directory path)
        /// </summary>
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
