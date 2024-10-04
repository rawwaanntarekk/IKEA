using LinkDev.IKEA.DAL.Models.Mails;
using LinkDev.IKEA.PL.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace LinkDev.IKEA.PL.Helpers
{
    public class MailSettings(IOptions<MailSetting> options) : IMailSettings
    {
        public void SendEmail(Email email)
        {
            var mail = new MimeMessage
            {
                Sender = MailboxAddress.Parse(options.Value.Email),
                Subject = email.Subject
            };

            mail.To.Add(MailboxAddress.Parse(email.To));
            mail.From.Add(new MailboxAddress(options.Value.DisplayName, options.Value.Email));

            var builder = new BodyBuilder
            {
                TextBody = email.Body
            };

            mail.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(options.Value.Host, options.Value.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(options.Value.Email, options.Value.Password);
            smtp.Send(mail);
            smtp.Disconnect(true);
        }
    }
}
