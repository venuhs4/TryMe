using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace GF_Messenger
{
    public class MailHelper
    {
        private static string FromAddress { get; set; }
        private static string ToAddress { get; set; }
        private static string CCAddress { get; set; }
        private static string SmtpServer { get; set; }
        private static string UserName { get; set; }
        private static string Password { get; set; }
        private static string Port { get; set; }
        private static string EnableMails { get; set; }

        static MailHelper()
        {
            FromAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            ToAddress = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
            CCAddress = ConfigurationManager.AppSettings["CcEmailAddress"].ToString();
            SmtpServer = ConfigurationManager.AppSettings["SMTPServer"].ToString();
            UserName = ConfigurationManager.AppSettings["SMTPUserName"].ToString();
            Password = ConfigurationManager.AppSettings["SMTPPassword"].ToString();
            Port = ConfigurationManager.AppSettings["SMTPPort"].ToString();
            EnableMails = ConfigurationManager.AppSettings["EnableMails"].ToString();
        }

        /// <summary>
        /// Sending mail in GF Networks
        /// </summary>
        /// <param name="recipeient">Receiver(Optional wil set to default recipients)</param>
        /// <param name="subject">Subject for the mail</param>
        /// <param name="bodyText">Content for the mail body</param>
        public static bool SendMail(string subject, string body)
        {
            try
            {
                if (EnableMails != "true")
                {
                    return false;
                }

                MailMessage mMailMessage = new MailMessage();
                mMailMessage.From = new MailAddress(FromAddress);

                foreach (var item in ToAddress.Split(';'))
                {
                    mMailMessage.To.Add(item);
                }

                foreach (var item in CCAddress.Split(';'))
                {
                    mMailMessage.CC.Add(item);
                }

                mMailMessage.Subject = subject;
                mMailMessage.Body = body;

                mMailMessage.Priority = MailPriority.High;

                SmtpClient mSmtpClient = new SmtpClient();
                mSmtpClient.Host = SmtpServer;

                if (Port != string.Empty)
                    mSmtpClient.Port = Convert.ToInt32(Port);// 587
                mSmtpClient.EnableSsl = true;

                ServicePointManager.ServerCertificateValidationCallback =
                delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };
                mSmtpClient.Send(mMailMessage);
            }
            catch (SmtpException ex)
            {
                string strError = ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                string strError = ex.Message;
                return false;
            }
            return true;
        }
    }
}