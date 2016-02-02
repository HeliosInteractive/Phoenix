namespace phoenix
{
    using System;
    using System.IO;
    using System.Net;
    using System.Linq;
    using System.Net.Mail;
    using System.Globalization;
    using System.Text.RegularExpressions;

    class ReportManager : IDisposable
    {
        private SmtpClient      m_Smtp;
        private EmailValidator  m_Validator;

        public ReportManager()
        {
            m_Validator = new EmailValidator();
            m_Smtp = new SmtpClient();

            m_Smtp.SendCompleted += (s, e) =>
            {
                if (e.Cancelled) {
                    Logger.Warn(string.Format("ReportManager canceled email operation ({0}).", e.UserState));
                } else if (e.Error != null) {
                    Logger.Error(string.Format("ReportManager email op encountered an error ({0}).", e.UserState));
                } else {
                    Logger.Info(string.Format("ReportManager sent a crash report email ({0}).", e.UserState));
                }
            };
        }

        public void SendEmail(string from, string from_password, string to, string subject, string body, string attachment = "")
        {
            from = from.Trim();
            to = to.Trim();

            if (String.IsNullOrEmpty(from_password) ||
                !m_Validator.IsValid(from) ||
                !m_Validator.IsValid(to))
                return;

            try
            {
                MailMessage message = new MailMessage();

                message.From = new MailAddress(from);
                message.To.Add(new MailAddress(to));
                message.Subject = subject;
                message.Body = body;

                if (!String.IsNullOrEmpty(attachment) && File.Exists(attachment))
                    message.Attachments.Add(new Attachment(attachment));

                ScreenCapture.TakeScreenShot();
                DirectoryInfo screenshot_directory = new DirectoryInfo(ScreenCapture.ScreenShotDirectory);
                FileInfo last_screenshot = screenshot_directory.GetFiles()
                             .OrderByDescending(f => f.LastWriteTime)
                             .First();

                if (last_screenshot.Exists)
                    message.Attachments.Add(new Attachment(last_screenshot.FullName));

                m_Smtp.Port = 587;
                m_Smtp.Host = "smtp.gmail.com";
                m_Smtp.EnableSsl = true;
                m_Smtp.UseDefaultCredentials = false;
                m_Smtp.Credentials = new NetworkCredential(from, from_password);
                m_Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                m_Smtp.SendAsyncCancel();
                m_Smtp.SendAsync(message, subject);
            }
            catch
            {
                Logger.Error("ReportManager failed to send the crash report.");
                return;
            }
        }

        // From: https://msdn.microsoft.com/en-us/library/01escwtf(v=vs.110).aspx
        internal class EmailValidator
        {
            bool invalid = false;

            public bool IsValid(string strIn)
            {
                invalid = false;
                if (String.IsNullOrEmpty(strIn))
                    return false;

                // Use IdnMapping class to convert Unicode domain names.
                try
                {
                    strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper,
                                          RegexOptions.None, TimeSpan.FromMilliseconds(200));
                }
                catch (RegexMatchTimeoutException)
                {
                    return false;
                }

                if (invalid)
                    return false;

                // Return true if strIn is in valid e-mail format.
                try
                {
                    return Regex.IsMatch(strIn,
                          @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                          @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                          RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                }
                catch (RegexMatchTimeoutException)
                {
                    return false;
                }
            }

            private string DomainMapper(Match match)
            {
                // IdnMapping class with default property values.
                IdnMapping idn = new IdnMapping();

                string domainName = match.Groups[2].Value;
                try
                {
                    domainName = idn.GetAscii(domainName);
                }
                catch (ArgumentException)
                {
                    invalid = true;
                }
                return match.Groups[1].Value + domainName;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing && m_Smtp != null)
                {
                    m_Smtp.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
