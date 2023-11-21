using FluentValidation;
using TruckWorld.Application.Common.Models;
using TruckWorld.Domain.Enums;

namespace TruckWorld.Infrastructure.Common.Validators;

public class SmsMessageValidator : AbstractValidator<SmsMessage>
{
    public SmsMessageValidator()
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
                RuleFor(message => message.SenderPhoneNumber).NotNull().NotEmpty();
                RuleFor(message => message.RecieverPhoneNumber).NotNull().NotEmpty();
                RuleFor(message => message.Message).NotNull().NotEmpty();
            });
    }
}