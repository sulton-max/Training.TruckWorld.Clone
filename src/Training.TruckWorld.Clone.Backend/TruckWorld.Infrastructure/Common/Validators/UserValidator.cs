using FluentValidation;
using Microsoft.Extensions.Options;
using TruckWorld.Application.Common.Settings;
using TruckWorld.Domain.Entities;
using TruckWorld.Domain.Enums;

namespace TruckWorld.Infrastructure.Common.Validators;

/// <summary>
/// Validator class for validating user data using FluentValidation.
/// </summary>
public class UserValidator : AbstractValidator<User>
{
    /// <summary>
    /// The validation settings used for user data validation.
    /// </summary>
    /// <param name="validationSettings"></param>
    public UserValidator(IOptions<ValidationSettings> validationSettings)
    {
        var validationSettingsValue = validationSettings.Value;

        RuleSet(EntityEvent.OnCreate.ToString(),
            () =>
            {
                RuleFor(code => code.Id).NotEqual(Guid.Empty);

                RuleFor(code => code.ExpiryTime)
                    .GreaterThanOrEqualTo(DateTime.UtcNow)
                    .LessThanOrEqualTo(DateTime.UtcNow.AddSeconds(validationSettingsValue.VerificationCodeExpiryTimeInSeconds));

                RuleFor(code => code.IsActive).Equal(true);

                RuleFor(code => code.VerificationLink).NotEmpty().Matches(validationSettingsValue.UrlRegexPattern);
            }
         );
    }
}