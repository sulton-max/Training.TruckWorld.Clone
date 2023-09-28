using Training.TruckWorld.Backend.Domain.Entities;

namespace Training.TruckWorld.Backend.Application.Notifications.Services;

public interface IEmailPlaceholderService
{
    ValueTask<(EmailTemplate, Dictionary<string, string>)> GetTemplateValues(Guid userId, EmailTemplate template);
}
