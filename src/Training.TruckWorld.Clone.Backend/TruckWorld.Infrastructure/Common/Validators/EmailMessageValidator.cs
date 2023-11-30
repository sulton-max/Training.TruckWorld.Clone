using FluentValidation;
using Microsoft.Extensions.Options;
using TruckWorld.Application.Common.Notificaitons.Models;
using TruckWorld.Application.Common.Settings;
using TruckWorld.Domain.Enums;

namespace TruckWorld.Infrastructure.Common.Validators;

public class EmailMessageValidator : AbstractValidator<EmailMessage>
{
    public EmailMessageValidator(IOptions<ValidationSettings> validation)
    {
        RuleSet(NotificationEvent.OnRendering.ToString(),
            () =>
            {
                RuleFor(message => message.Template).NotNull();
                RuleFor(message => message.Variables).NotNull();
            });

        RuleSet(NotificationEvent.OnSending.ToString(),
            () =>
            {
                RuleFor(message => message.RecieverEmailAddress).NotNull().NotEmpty().Matches(validation.Value.EmailRegexPattern);
                RuleFor(message => message.SenderEmailAddress).NotNull().NotEmpty().Matches(validation.Value.EmailRegexPattern);
                RuleFor(message => message.Subject).NotNull().NotEmpty();
                RuleFor(message => message.Body).NotNull().NotEmpty();
            });
    }
}