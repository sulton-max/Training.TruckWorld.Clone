using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Infrastructure.Notifications.Models;

namespace Training.TruckWorld.Backend.Application.Notifications.Services;

public interface IEmailSenderService
{
    Task<bool> SendEmailAsync(EmailMessage emailMessage);//implement it in EmailSenderService
}
