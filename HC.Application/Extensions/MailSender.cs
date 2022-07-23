using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Extensions
{
    public class MailSender
    {
        public static void SendMail(string email, string subject,string message)
        {
            //Sender
            var mail = new MailMessage();
            mail.From = new MailAddress("no-reply@hotcatrestaurant.com","HotCat Restaurant"); //https://mailtrap.io adresine test maili olarak gönderiyorum.
            mail.To.Add(email);
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = message;
            //Smtp
            try
            {
                SmtpClient smtp = new SmtpClient("smtp.mailtrap.io", 587);
                smtp.Credentials = new NetworkCredential("d502980aa76f2c", "62b9c27dd34b83");
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
