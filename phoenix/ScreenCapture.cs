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

        public static string TakeScreenShot(string image_path = "")
        {
            string path = string.Empty;

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
                        if (String.IsNullOrWhiteSpace(image_path))
                            path = Path.Combine(ScreenShotDirectory, string.Format("{0}.png", DateTime.Now.Ticks));
                        else
                            path = image_path;

                        bmp.Save(path, ImageFormat.Png);
                    }
                }
            }

            return path;
        }
    }
}
