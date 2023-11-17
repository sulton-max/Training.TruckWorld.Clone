using FluentValidation;

using Microsoft.Extensions.Options;

using TruckWorld.Application.Common.Settings;
using TruckWorld.Domain.Entities;

namespace TruckWorld.Infrastructure.Common.Validators;

/// <summary>
/// Validator class for validating user data using FluentValidation.
/// </summary>
public class UserValidator : AbstractValidator<User>
{
    public UserValidator(IOptions<ValidationSettings> validationSettings)
    {
        var validationSettingsValue = validationSettings.Value;

        RuleFor(user => user.EmailAddress)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(64)
            .Matches(validationSettingsValue.EmailRegexPattern);

        RuleFor(user => user.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(128);
    }
}