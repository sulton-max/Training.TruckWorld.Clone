using FluentValidation;
using Microsoft.Extensions.Options;
using TruckWorld.Application.Common.Notificaitons.Models;
using TruckWorld.Application.Common.Settings;
using TruckWorld.Domain.Enums;

namespace TruckWorld.Infrastructure.Common.Validators;

public class SmsMessageValidator : AbstractValidator<SmsMessage>
{
    public SmsMessageValidator(IOptions<ValidationSettings> validatorSettings)
    {
        RuleSet(NotificationEvent.OnRedering.ToString(),
            () =>
            {
                RuleFor(message => message.Template).NotNull();
                RuleFor(message => message.Variables).NotNull();
            });

        RuleSet(NotificationEvent.OnSending.ToString(),
            () =>
            {
                RuleFor(message => message.SenderPhoneNumber).NotNull().NotEmpty().Matches(validatorSettings.Value.PhoneNumberRegexPattern);
                RuleFor(message => message.RecieverPhoneNumber).NotNull().NotEmpty().Matches(validatorSettings.Value.PhoneNumberRegexPattern);
                RuleFor(message => message.Message).NotNull().NotEmpty();
            });
    }
}