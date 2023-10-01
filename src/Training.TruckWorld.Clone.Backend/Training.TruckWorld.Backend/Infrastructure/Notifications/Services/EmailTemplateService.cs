﻿using System.Linq.Expressions;
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
    public async ValueTask<EmailTemplate> CreateAsync(EmailTemplate emailTemplate, bool saveChanges = true, CancellationToken cancellationToken = default)
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

    public ValueTask<EmailTemplate?> GetByIdAsync(Guid id)
    {
        var emailTemplate = _appDateContext.EmailTemplates.FirstOrDefault(emailTemplate => emailTemplate.Id == id);
        return new ValueTask<EmailTemplate?>(emailTemplate);
    }

    public ValueTask<ICollection<EmailTemplate>> GetAsync(IEnumerable<Guid> ids)
    {
        var emailTemlates = _appDateContext.EmailTemplates.Where(emailtemplate => ids.Contains(emailtemplate.Id));
        return new ValueTask<ICollection<EmailTemplate>>(emailTemlates.ToList());
    }

    public async ValueTask<EmailTemplate> UpdateAsync(EmailTemplate emailTemplate, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundEmailTemplate = _appDateContext.EmailTemplates.FirstOrDefault(searched => searched.Id == emailTemplate.Id);
        if (foundEmailTemplate is null)
            throw new EntityNotFoundException(typeof(EmailTemplate), foundEmailTemplate.Id);
        foundEmailTemplate.Subject = emailTemplate.Subject;
        foundEmailTemplate.Body = emailTemplate.Body;
        foundEmailTemplate.ModifiedDate = DateTime.UtcNow;
        if (saveChanges)
            await _appDateContext.SaveChangesAsync();
        return foundEmailTemplate;
    }

    public async ValueTask<EmailTemplate> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundEmailTemplate = await GetByIdAsync(id);
        if (foundEmailTemplate is null)
            throw new EntityNotFoundException(typeof(EmailTemplate), foundEmailTemplate.Id);

        if (foundEmailTemplate.IsDeleted)
            throw new EntityDeletedException(typeof(EmailTemplate), foundEmailTemplate.Id);

        foundEmailTemplate.IsDeleted = true;
        foundEmailTemplate.DeletedDate  = DateTime.UtcNow;
        if (saveChanges)
            await _appDateContext.SaveChangesAsync();
        return foundEmailTemplate;
    }

    public async ValueTask<EmailTemplate> DeleteAsync(EmailTemplate emailTemplate, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundEmailTemplate = await GetByIdAsync(emailTemplate.Id);
        if (foundEmailTemplate is null)
            throw new EntityNotFoundException(typeof(EmailTemplate), foundEmailTemplate.Id);

        if (foundEmailTemplate.IsDeleted)
            throw new EntityDeletedException(typeof(EmailTemplate), foundEmailTemplate.Id);

        foundEmailTemplate.IsDeleted = true;
        foundEmailTemplate.DeletedDate = DateTime.UtcNow;
        if (saveChanges)
            await _appDateContext.SaveChangesAsync();
        return foundEmailTemplate;
    }
}