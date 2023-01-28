using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RIArchitecture.Application.Contracts.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using RIArchitecture.Shared.Extensions;
using RIArchitecture.Application.Contracts.Utility;

namespace RIArchitecture.Application.Utility
{
    public class EmailAppService : IEmailAppService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailAppService> _logger;

        public EmailAppService(
            IOptions<EmailSettings> emailSettings,
            ILogger<EmailAppService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }
        public Task SendEmailAsync(EmailInputDto input)
        {
            Execute(input).Wait();
            return Task.FromResult(0);
        }

        public async Task Execute(EmailInputDto input)
        {
            try
            {
                if (input?.Emails.Count == 0)
                    throw new InvalidOperationException("To email cannot be null");

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail, "Sapney Mason")
                };

                foreach (var email in input.Emails)
                {
                    mail.To.Add(new MailAddress(email));
                }

                mail.Subject = "Sapney Mason - " + input.Subject;
                mail.Body = input.Message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while sending email : {ex.Message}");
            }
        }

        public static string CombineAllEmailIds(List<string> input)
        {
            foreach (var item in input)
            {
                item.RemoveAllWhitespace();
                if (string.IsNullOrEmpty(item))
                    input.Remove(item);
            }
            return input.Count > 0 ? string.Join(';', input) : null;
        }
    }
}
