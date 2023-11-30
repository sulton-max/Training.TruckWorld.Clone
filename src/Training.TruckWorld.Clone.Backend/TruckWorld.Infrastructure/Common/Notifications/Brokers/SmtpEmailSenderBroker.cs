using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using TruckWorld.Application.Common.Notificaitons.Brokers;
using TruckWorld.Application.Common.Notificaitons.Models;
using TruckWorld.Infrastructure.Common.Settings;

namespace TruckWorld.Infrastructure.Common.Notifications.Brokers;

public class SmtpEmailSenderBroker : IEmailSenderBroker
{
    private readonly SmtpEmailSenderSettings _settings;

    public SmtpEmailSenderBroker(IOptions<SmtpEmailSenderSettings> smtpEmailSenderSettings) =>
        _settings = smtpEmailSenderSettings.Value;

    public async ValueTask<bool> SendAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default)
    {
        emailMessage.SenderEmailAddress ??= _settings.CredentialAddress;

        var mail = new MailMessage(emailMessage.SenderEmailAddress, emailMessage.RecieverEmailAddress);

        mail.Subject = emailMessage.Subject;
        mail.Body = emailMessage.Body;
        mail.IsBodyHtml = true;

        var smtpClient = new SmtpClient(_settings.Host, _settings.Port);
        smtpClient.Credentials = new NetworkCredential(_settings.CredentialAddress, _settings.Password);
        smtpClient.EnableSsl = true;

        smtpClient.Send(mail);

        return true;
    }
}