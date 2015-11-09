using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;
using System.IO;

namespace OnlineShopping.Web.Utilites
{
    public class EmailUtility
    {
        public static void SendEmail(string toEmail, string subject, string body, List<string> ccEmail = null)
        {

           
            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], ConfigurationManager.AppSettings["displayName"]);
           
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient(ConfigurationManager.AppSettings["smtpServer"], Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]));
            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = ConfigurationManager.AppSettings["userId"],
                Password = ConfigurationManager.AppSettings["password"]
            };
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);
        }
    }
}