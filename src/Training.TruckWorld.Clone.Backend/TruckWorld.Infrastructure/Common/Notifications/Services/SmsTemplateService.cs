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

public class SmsTemplateService : ISmsTemplateService
{
    private readonly ISmsTemplateRepository _smsTemplateRepository;
    private readonly IValidator<SmsTemplate> _smsTemplateValidator;

    public SmsTemplateService(
        ISmsTemplateRepository smsTemplateRepository,
        IValidator<SmsTemplate> smsTemplateValidator)
    {
        _smsTemplateRepository = smsTemplateRepository;
        _smsTemplateValidator = smsTemplateValidator;
    }

    public IQueryable<SmsTemplate> Get(
        Expression<Func<SmsTemplate, bool>>? predicate = default,
        bool asNoTracking = false
        ) =>
            _smsTemplateRepository.Get(predicate, asNoTracking);

    public async ValueTask<IList<SmsTemplate>> GetByFilterAsync(
        FilterPagination pagination,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    ) =>
            await Get(asNoTracking: asNoTracking)
                    .ApplyPagination(pagination)
                    .ToListAsync(cancellationToken: cancellationToken);

    public async ValueTask<SmsTemplate?> GetByTypeAsync(
        NotificationTemplateType templateType,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
        ) =>
            await _smsTemplateRepository.Get(template => template.TemplateType == templateType, asNoTracking: asNoTracking)
                  .SingleOrDefaultAsync(cancellationToken);


    public ValueTask<SmsTemplate> CreateAsync(
        SmsTemplate smsTemplate,
        bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        var validationResult = _smsTemplateValidator.Validate(smsTemplate);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return _smsTemplateRepository.CreateAsync(smsTemplate, saveChanges, cancellationToken);
    }
}
