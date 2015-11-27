using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace phoenix
{
    class ScreenCapture
    {
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

                    if (!Directory.Exists("captures"))
                    {
                        Directory.CreateDirectory("captures");
                    }

                    if (Directory.Exists("captures"))
                    {
                        bmp.Save(string.Format("captures\\{0}.png", DateTime.Now.Ticks), ImageFormat.Png);
                    }
                }
            }
        }
    }
}
