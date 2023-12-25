using FluentValidation;
using TruckWorld.Application.Common.Notifications.Brokers;
using TruckWorld.Application.Common.Notifications.Models;
using TruckWorld.Application.Common.Notifications.Services;
using TruckWorld.Domain.Enums;
using TruckWorld.Domain.Extensions;

namespace TruckWorld.Infrastructure.Common.Notifications.Services;

public class SmsSenderService(IEnumerable<ISmsSenderBroker> smsSenderBroker, IValidator<SmsMessage> smsSenderValidator)
    : ISmsSenderService
{
    private IEnumerable<ISmsSenderBroker> _smsSenderBroker => smsSenderBroker;
    private IValidator<SmsMessage> _smsSenderValidator => smsSenderValidator;

    public async ValueTask<bool> SendAsync(SmsMessage smsMessage, CancellationToken cancellationToken = default)
    {
        var validationResult = _smsSenderValidator.Validate(smsMessage,
            options => options
            .IncludeRuleSets(NotificationEvent.OnSending.ToString()));

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        foreach (var broker in _smsSenderBroker)
        {
            var sendNotification = () => broker.SendAsync(smsMessage, cancellationToken);

            var result = await sendNotification.GetValueAsync();

            smsMessage.IsSuccessfull = result.IsSuccess;
            smsMessage.ErrorMessage = result.Exception?.Message;

            if (smsMessage.IsSuccessfull)
                return true;
        }

        return false;
    }
}