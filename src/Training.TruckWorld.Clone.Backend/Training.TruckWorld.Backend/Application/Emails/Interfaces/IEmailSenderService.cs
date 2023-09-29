using Training.TruckWorld.Backend.Domain.Entities;

namespace Training.TruckWorld.Backend.Application.Emails.Interfaces;

public interface IEmailSenderService
{
    ValueTask<Email> SendEmailAsync(Email email);
}
