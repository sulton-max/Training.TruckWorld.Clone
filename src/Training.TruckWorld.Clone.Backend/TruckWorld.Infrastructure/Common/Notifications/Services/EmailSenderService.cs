using FluentValidation;
using TruckWorld.Application.Common.Brokers;
using TruckWorld.Application.Common.Models;
using TruckWorld.Application.Common.Services;
using TruckWorld.Domain.Enums;
using TruckWorld.Domain.Extensions;

namespace TruckWorld.Infrastructure.Common.Notifications.Services;

public class EmailSenderService : IEmailSenderService
{
    private readonly IEnumerable<IEmailSenderBroker> _mailSenderBroker;
    private readonly IValidator<EmailMessage> _emailMessageValidator;

    public EmailSenderService(IEnumerable<IEmailSenderBroker> emailSenderBrokers, IValidator<EmailMessage> validator)
    {
        _emailMessageValidator = validator;
        _mailSenderBroker = emailSenderBrokers;
    }

    public async ValueTask<bool> SendAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default)
    {
        var validationResult = _emailMessageValidator.Validate(emailMessage,
            options => options.IncludeRuleSets(NotificationEvent.OnSending.ToString()));

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        foreach (var broker in _mailSenderBroker)
        {
            var sendNotificaitonTask = () => broker.SendAsync(emailMessage, cancellationToken);

            var result = await sendNotificaitonTask.GetValueAsync();

            emailMessage.IsSuccessfull = result.IsSuccess;
            emailMessage.ErrorMessage = result.Exception?.Message;

            return emailMessage.IsSuccessfull;
        }

        return false;
    }
}