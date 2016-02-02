namespace phoenix
{
    using System;
    using System.IO;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Drawing.Imaging;

    class ScreenCapture
    {
        public static string ScreenShotDirectory
        {
            get { return "captures"; }
        }

        public static void TakeScreenShot()
        {
            using (Bitmap bmp = new Bitmap(
                Screen.PrimaryScreen.Bounds.Width,
                Screen.PrimaryScreen.Bounds.Height))
            {
                using (Graphics graphics = Graphics.FromImage(bmp))
                {
                    graphics.CopyFromScreen(
                        Screen.PrimaryScreen.Bounds.X,
                        Screen.PrimaryScreen.Bounds.Y,
                        0, 0,
                        bmp.Size,
                        CopyPixelOperation.SourceCopy);

                    if (!Directory.Exists(ScreenShotDirectory))
                    {
                        Directory.CreateDirectory(ScreenShotDirectory);
                    }

                    if (Directory.Exists(ScreenShotDirectory))
                    {
                        bmp.Save(string.Format("{0}\\{1}.png", ScreenShotDirectory, DateTime.Now.Ticks), ImageFormat.Png);
                    }
                }
            }
        }
    }
}
