using System.Linq.Expressions;
using Training.TruckWorld.Backend.Application.Notifications.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Persistence.DataContexts;
using Training.TruckWorld.Backend.Domain.Exceptions;

namespace Training.TruckWorld.Backend.Infrastructure.Notifications.Services;

public class EmailTemplateService : IEmailTemplateService
{
    private readonly IDataContext _appDateContext;

    public EmailTemplateService(IDataContext appDateContext)
    {
        _appDateContext = appDateContext;
    }

    public async ValueTask<EmailTemplate> CreateAsync(EmailTemplate emailTemplate, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        await _appDateContext.EmailTemplates.AddAsync(emailTemplate, cancellationToken);

        if (saveChanges)
            await _appDateContext.SaveChangesAsync();

        return emailTemplate;
    }

    public IQueryable<EmailTemplate> Get(Expression<Func<EmailTemplate, bool>> expression)
    {
        return _appDateContext.EmailTemplates.Where(expression.Compile()).AsQueryable();
    }

    public async ValueTask<EmailTemplate?> GetByIdAsync(Guid id)
    => await _appDateContext.EmailTemplates.FindAsync(id);

    public ValueTask<ICollection<EmailTemplate>> GetAsync(IEnumerable<Guid> ids)
    {
        var emailTemlates = _appDateContext.EmailTemplates.Where(emailtemplate => ids.Contains(emailtemplate.Id));

        return new ValueTask<ICollection<EmailTemplate>>(emailTemlates.ToList());
    }

    public async ValueTask<EmailTemplate> UpdateAsync(EmailTemplate emailTemplate, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        var foundEmailTemplate =
            _appDateContext.EmailTemplates.FirstOrDefault(searched => searched.Id == emailTemplate.Id)
            ?? throw new EntityNotFoundException(typeof(EmailTemplate));


        foundEmailTemplate.Subject = emailTemplate.Subject;
        foundEmailTemplate.Body = emailTemplate.Body;

        await _appDateContext.EmailTemplates.UpdateAsync(foundEmailTemplate, cancellationToken);

        if (saveChanges)
            await _appDateContext.SaveChangesAsync();

        return foundEmailTemplate;
    }

    public async ValueTask<EmailTemplate> DeleteAsync(Guid id, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        var foundEmailTemplate = await GetByIdAsync(id) ?? throw new EntityNotFoundException(typeof(EmailTemplate));

        if (foundEmailTemplate.IsDeleted)
            throw new EntityDeletedException(typeof(EmailTemplate), foundEmailTemplate.Id);

        foundEmailTemplate.IsDeleted = true;

        await _appDateContext.EmailTemplates.RemoveAsync(foundEmailTemplate, cancellationToken);

        if (saveChanges)
            await _appDateContext.SaveChangesAsync();

        return foundEmailTemplate;
    }

    public  ValueTask<EmailTemplate> DeleteAsync(EmailTemplate emailTemplate, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return DeleteAsync(emailTemplate.Id, saveChanges, cancellationToken);
    }
}