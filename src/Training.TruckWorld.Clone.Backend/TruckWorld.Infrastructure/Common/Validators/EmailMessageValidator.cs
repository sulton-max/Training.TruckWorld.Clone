using FluentValidation;
using TruckWorld.Application.Common.Models;
using TruckWorld.Domain.Enums;

namespace TruckWorld.Infrastructure.Common.Validators;

public class EmailMessageValidator : AbstractValidator<EmailMessage>
{
    public EmailMessageValidator()
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
                RuleFor(message => message.RecieverEmailAddress).NotNull().NotEmpty();
                RuleFor(message => message.SenderEmailAddress).NotNull().NotEmpty();
                RuleFor(message => message.Subject).NotNull().NotEmpty();
                RuleFor(message => message.Body).NotNull().NotEmpty();
            });
    }
}