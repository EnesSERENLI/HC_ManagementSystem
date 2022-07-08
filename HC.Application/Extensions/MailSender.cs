using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Extensions
{
    public class MailSender
    {
        public static void SendMail(string email, string subject,string message)
        {
            //Sender
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("yzl3156yzl@gmail.com", "HotCat Restaurant");
            mail.To.Add(email);
            mail.Subject = subject;
            mail.Body = message;
            mail.IsBodyHtml = true;

            //Smtp
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential("yzl3156yzl@gmail.com", "enter your password");
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;

            smtp.Send(mail);
        }
    }
}
