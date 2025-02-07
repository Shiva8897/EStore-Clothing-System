using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EStore.Application.Interfaces;

namespace EStore.Application.Services
{
    public  class EmailService:IEmailService
    {
        public void SendMailNotification(string toEmail, string subject, string body)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("snasreen877@gmail.com", GetPassword());
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("snasreen877@gmail.com");
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            smtpClient.Send(mailMessage);
        }
        private string GetPassword()
        {
            return "onbjfvdyiltbrnmr";
        }
    }
}
