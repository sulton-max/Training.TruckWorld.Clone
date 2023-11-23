﻿using FluentValidation;
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

        RuleSet (
            EntityEvent.OnCreate.ToString(),
            () =>
            {
                RuleFor(code => code.Id).NotEqual(Guid.Empty);

                RuleFor(user => user.EmailAddress)
                    .NotEmpty()
                    .MinimumLength(5)
                    .MaximumLength(64)
                    .Matches(validationSettingsValue.EmailRegexPattern);

                RuleFor(user => user.FirstName)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(64)
                    .Matches(validationSettingsValue.NameRegexPattern)
                    .WithMessage("First name is not valid!");

                RuleFor(user => user.LastName)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(64)
                    .Matches(validationSettingsValue.NameRegexPattern)
                    .WithMessage("Last name is not valid!");

                RuleFor(user => user.PasswordHash)
                    .NotEmpty()
                    .MinimumLength(8)
                    .MaximumLength(64)
                    .Matches(validationSettingsValue.PasswordRegexPattern);
            }
        );
    }
}