﻿using System;
using System.Windows.Forms;

namespace phoenix
{
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
    }
}
