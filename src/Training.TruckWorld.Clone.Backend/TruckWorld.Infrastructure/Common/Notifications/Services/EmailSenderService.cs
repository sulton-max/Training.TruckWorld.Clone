using FluentValidation;
using TruckWorld.Application.Common.Notificaitons.Brokers;
using TruckWorld.Application.Common.Notificaitons.Models;
using TruckWorld.Application.Common.Notificaitons.Services;
using TruckWorld.Domain.Enums;
using TruckWorld.Domain.Extensions;

namespace TruckWorld.Infrastructure.Common.Notifications.Services;

public class EmailSenderService(IEnumerable<IEmailSenderBroker> emailSenderBrokers, IValidator<EmailMessage> validator)
    : IEmailSenderService
{
    private IEnumerable<IEmailSenderBroker> _mailSenderBroker => emailSenderBrokers;
    private IValidator<EmailMessage> _emailMessageValidator => validator;

    public async ValueTask<bool> SendAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default)
    {
        var validationResult = _emailMessageValidator.Validate(emailMessage,
            options => options
            .IncludeRuleSets(NotificationEvent.OnRedering.ToString())
            .IncludeRuleSets(NotificationEvent.OnSending.ToString()));

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        foreach (var broker in _mailSenderBroker)
        {
            var sendNotificaitonTask = () => broker.SendAsync(emailMessage, cancellationToken);

            var result = await sendNotificaitonTask.GetValueAsync();

            emailMessage.IsSuccessfull = result.IsSuccess;
            emailMessage.ErrorMessage = result.Exception?.Message;

            if (emailMessage.IsSuccessfull)
                return true;
        }

        return false;
    }
}