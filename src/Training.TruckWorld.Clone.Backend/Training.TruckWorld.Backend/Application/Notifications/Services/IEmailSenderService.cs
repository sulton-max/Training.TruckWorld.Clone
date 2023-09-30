using Training.TruckWorld.Backend.Infrastructure.Notifications.Models;

namespace Training.TruckWorld.Backend.Application.Notifications.Services;

public interface IEmailSenderService
{
    ValueTask<bool> SendEmailAsync(EmailMessage emailMessage);//implement it in EmailSenderService
}
