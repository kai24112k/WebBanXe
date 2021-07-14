using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace WebBanXe.Service
{
    public class EmailService
    {
        public void sendEmail(string emailAddress, string title, string message)
        {
            string myEmail = "xemaydhcv@gmail.com";
            string myPassword = "Xemaydhcv@123";

            var loginInfo = new NetworkCredential(myEmail, myPassword);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);

            msg.From = new MailAddress(myEmail);
            msg.To.Add(new MailAddress(emailAddress));
            msg.Subject = title;
            msg.Body = message;
            msg.IsBodyHtml = true;

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);
        }
    }
}