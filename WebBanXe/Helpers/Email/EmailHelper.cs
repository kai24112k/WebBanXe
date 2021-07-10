using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace WebBanXe.Helpers.Email
{
    public class EmailHelper
    {
        public static void SendMail(string toEmail, string subject, string body)
        {
            var smtp = new SmtpClient
            {
                Host = EmailSetting.SMTP,
                Port = EmailSetting.Port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(EmailSetting.Username, EmailSetting.Password)
            };

            // add from,to mailaddresses
            MailAddress from = new MailAddress(EmailSetting.Username, "XE MÁY DHCV");
            MailAddress to = new MailAddress(toEmail);
            MailMessage myMail = new MailMessage(from, to);

            // set subject and encoding
            myMail.Subject = subject;
            myMail.SubjectEncoding = System.Text.Encoding.UTF8;
            // set body-message and encoding
            myMail.Body = body;
            myMail.BodyEncoding = System.Text.Encoding.UTF8;

            // text or html
            myMail.IsBodyHtml = true;

            smtp.Send(myMail);
        }
    }
}