namespace Training.TruckWorld.Backend.Application.Notifications.Services;

public interface IEmailManagementService
{
    ValueTask<bool> SendEmailAsync(Guid userId, Guid templateId);
    
    IQueryable<ValueTask<bool>> SendEmailAsync(Guid userId, string templateCategory);
}