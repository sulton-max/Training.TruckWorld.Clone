using FluentValidation;
using TruckWorld.Domain.Entities;
using TruckWorld.Domain.Enums;

namespace TruckWorld.Infrastructure.Common.Validators;

/// <summary>
/// Validator class for validating emailTemplate data using FluentValidation.
/// </summary>
public class EmailTemplateValidator : AbstractValidator<EmailTemplate>
{
    /// <summary>
    /// The validation settings emailTemplate for emailTemplate data validation.
    /// </summary>
    public EmailTemplateValidator()
    {
        RuleFor(template => template.Content)
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(256);

        RuleFor(template => template.Type)
            .Equal(NotificationType.Email);
    }
}
