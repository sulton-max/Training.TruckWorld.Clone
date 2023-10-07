using System.Net;
using System.Net.Mail;
using Training.TruckWorld.Backend.Application.Notifications.Services;
using Training.TruckWorld.Backend.Infrastructure.Notifications.Models;

namespace Training.TruckWorld.Backend.Infrastructure.Notifications.Services;

public class EmailSenderService : IEmailSenderService
{
    public SmtpClient SmtpClientInstance { get; init; }

    public EmailSenderService()
    {
        SmtpClientInstance = new SmtpClient("smtp.gmail.com", 587);
        SmtpClientInstance.Credentials = new NetworkCredential("sultonbek.rakhimov.recovery@gmail.com", "szabguksrhwsbtie");
        SmtpClientInstance.EnableSsl = true;
    }
    public Task<bool> SendEmailAsync(EmailMessage emailMessage)
    {
        return Task.Run(async () =>
        {
            var result = true;
            try
            {
                var smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("sultonbek.rakhimov.recovery@gmail.com", "szabguksrhwsbtie");
                smtp.EnableSsl = true;

                var mail = new MailMessage("sultonbek.rakhimov@gmail.com", emailMessage.ReceiverAddress);
                mail.Subject = emailMessage.Subject;
                mail.Body = emailMessage.Body;

                emailMessage.IsSent = result;
                emailMessage.SentTime = DateTime.UtcNow;

                await smtp.SendMailAsync(mail);
            }
            catch (Exception e)
            {
                result = false;
            }

            return result;
        });
    }
}
