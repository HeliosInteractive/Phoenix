namespace phoenix
{
    using System;
    using System.IO;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Drawing.Imaging;

    /// <summary>
    /// Class, responsible for taking a single screen shot of the primary
    /// monitor. ** No multi monitor support yet **
    /// </summary>
    class ScreenCapture
    {
        /// <summary>
        /// Returns an absolute path to where screen shots are stored
        /// </summary>
        public static string ScreenShotDirectory
        {
            get { return Path.Combine(Program.Directory, "captures"); }
        }

        /// <summary>
        /// Attempts to take a screen shot. Saves to ScreenShotDirectory
        /// if supplied image_path is null or empty
        /// </summary>
        /// <param name="image_path">optional path for screen shot to be saved</param>
        /// <returns>actual path the screen shot was saved to</returns>
        public static string TakeScreenShot(string image_path = "")
        {
            string path = string.Empty;

            try
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
                            Directory.CreateDirectory(ScreenShotDirectory);

                        if (String.IsNullOrWhiteSpace(image_path))
                            path = Path.Combine(ScreenShotDirectory,
                                string.Format("{0}.png", DateTime.Now.Ticks));
                        else
                            path = image_path;

                        bmp.Save(path, ImageFormat.Png);
                    }
                }
            }
            catch
            {
                path = string.Empty;
                Logger.ScreenCapture.Error("Capturing screen shot failed.");
            }

            return path;
        }
    }
}
