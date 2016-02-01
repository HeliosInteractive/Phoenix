using System.IO;
using System.Net;
using System.Linq;
using System.Net.Mail;

namespace phoenix
{
    class ReportManager
    {
        public static void Send(string from, string from_password, string to, string subject, string body, string attachment = "")
        {
            if (from == string.Empty ||
                from_password == string.Empty ||
                to == string.Empty)
                return;

            from = from.Trim();
            to = to.Trim();

            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                message.From = new MailAddress(from);
                message.To.Add(new MailAddress(to));
                message.Subject = subject;
                message.Body = body;

                if (attachment != string.Empty && File.Exists(attachment))
                    message.Attachments.Add(new Attachment(attachment));

                ScreenCapture.TakeScreenShot();
                DirectoryInfo screenshot_directory = new DirectoryInfo(ScreenCapture.ScreenShotDirectory);
                FileInfo last_screenshot = screenshot_directory.GetFiles()
                             .OrderByDescending(f => f.LastWriteTime)
                             .First();

                if (last_screenshot.Exists)
                    message.Attachments.Add(new Attachment(last_screenshot.FullName));

                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(from, from_password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.SendMailAsync(message);
            }
            catch
            {
                Logger.Error("ReportManager failed to send the crash report.");
                return;
            }
        }
    }
}
