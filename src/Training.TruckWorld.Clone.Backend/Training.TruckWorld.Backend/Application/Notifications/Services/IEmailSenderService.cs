using Training.TruckWorld.Backend.Domain.Entities;

namespace Training.TruckWorld.Backend.Application.Notifications.Services;

public interface IEmailSenderService
{
    ValueTask<bool> SendEmailAsync(Email emailMessage);//implement it in EmailSenderService
}
