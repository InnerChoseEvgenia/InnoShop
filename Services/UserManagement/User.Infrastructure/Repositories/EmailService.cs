using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using User.Application.Dto;

namespace User.Infrastructure.Repositories
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> SendEmailAsync(EmailSendDto emailSend)
        {
            try
            {
                var username = _config["SMTP:Username"];
                var password = _config["SMTP:Password"];


                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(username, password)
                };

                var message = new MailMessage(from: username, to: emailSend.To, subject: emailSend.Subject, body: emailSend.Body);

                message.IsBodyHtml = true;
                await client.SendMailAsync(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
