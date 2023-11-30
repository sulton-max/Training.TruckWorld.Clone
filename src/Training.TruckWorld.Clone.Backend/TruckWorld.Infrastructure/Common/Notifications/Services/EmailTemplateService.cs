using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TruckWorld.Application.Common.Models.Querying;
using TruckWorld.Application.Common.Notifications.Services;
using TruckWorld.Application.Common.Querying.Extensions;
using TruckWorld.Domain.Entities;
using TruckWorld.Domain.Enums;
using TruckWorld.Persistence.Repositories.Interface;

namespace TruckWorld.Infrastructure.Common.Notifications.Services;

public class EmailTemplateService : IEmailTemplateService
{
    private readonly IEmailTemplateRepository _emailTemplateRepository;
    private readonly IValidator<EmailTemplate> _emailTemplateValidator;

    public EmailTemplateService(
        IEmailTemplateRepository emailTemplateRepository,
         IValidator<EmailTemplate> emailTemplateValidator)
    {
        _emailTemplateRepository = emailTemplateRepository;
        _emailTemplateValidator = emailTemplateValidator;
    }


    public IQueryable<EmailTemplate> Get(Expression<Func<EmailTemplate, bool>>? predicate = default,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default)
        =>
            _emailTemplateRepository.Get(predicate, asNoTracking);

    public async ValueTask<IList<EmailTemplate>> GetByFilterAsync(
        FilterPagination paginationOptions,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default)
        =>
        await Get(asNoTracking: asNoTracking)
            .ApplyPagination(paginationOptions)
            .ToListAsync(cancellationToken: cancellationToken);

    public async ValueTask<EmailTemplate?> GetBytypeAsync(
        NotificationTemplateType templateType,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
        ) =>
            await _emailTemplateRepository.Get(template => templateType == templateType, asNoTracking)
            .SingleOrDefaultAsync(cancellationToken);

    public ValueTask<EmailTemplate> CreateAsync(
        EmailTemplate emailTemplate,
        bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        var validationResult = _emailTemplateValidator.Validate(emailTemplate);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return _emailTemplateRepository.CreateAsync(emailTemplate, saveChanges, cancellationToken);
    }
}
