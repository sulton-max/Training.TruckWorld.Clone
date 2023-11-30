using FluentValidation;
using TruckWorld.Domain.Entities;
using TruckWorld.Domain.Enums;

namespace TruckWorld.Infrastructure.Common.Validators;

/// <summary>
/// Validator class for validating smsTemplate data using FluentValidation.
/// </summary>
public class SmsTemplateValidator : AbstractValidator<SmsTemplate>
{
    /// <summary>
    /// The validation settings smsTemplate for smsTemplate data validation.
    /// </summary>
    public SmsTemplateValidator()
    {
        RuleFor(template => template.Content)
            .NotEmpty()
            .WithMessage("Sms template content is required")
            .MinimumLength(10)
            .WithMessage("Sms template content must be at least 10 characters long")
            .MaximumLength(256)
            .WithMessage("Sms template content must be at most 256 characters long");

        RuleFor(template => template.Type)
            .Equal(NotificationType.Sms)
            .WithMessage("Sms template notification type must be Sms");
    }
}
