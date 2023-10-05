using System.Linq.Expressions;
using Training.TruckWorld.Backend.Domain.Entities;

namespace Training.TruckWorld.Backend.Application.Notifications.Services;

public interface IEmailTemplateService
{
    IQueryable<EmailTemplate> Get(Expression<Func<EmailTemplate, bool>> expression);
    
    ValueTask<ICollection<EmailTemplate>> GetAsync(IEnumerable<Guid> ids);
    
    ValueTask<EmailTemplate?> GetByIdAsync(Guid id);
    
    ValueTask<EmailTemplate> CreateAsync(EmailTemplate emailTemplate, bool saveChanges = true, CancellationToken cancellationToken = default);
    
    ValueTask<EmailTemplate> UpdateAsync(EmailTemplate emailTemplate, bool saveChanges = true, CancellationToken cancellationToken = default);
    
    ValueTask<EmailTemplate> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);
    
    ValueTask<EmailTemplate> DeleteAsync(EmailTemplate emailTemplate, bool saveChanges = true, CancellationToken cancellationToken = default);
}