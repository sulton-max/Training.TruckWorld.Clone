﻿using FluentValidation;
using TruckWorld.Application.Common.Brokers;
using TruckWorld.Application.Common.Models;
using TruckWorld.Application.Common.Services;
using TruckWorld.Domain.Enums;
using TruckWorld.Domain.Extensions;

namespace TruckWorld.Infrastructure.Common.Notifications.Services;

public class SmsSenderService : ISmsSenderService
{
    private readonly IEnumerable<ISmsSenderBroker> _smsSenderBroker;
    private readonly IValidator<SmsMessage> _smsSenderValidator;

    public SmsSenderService(IEnumerable<ISmsSenderBroker> smsSenderBroker, IValidator<SmsMessage> smsSenderValidator)
    {
        _smsSenderBroker = smsSenderBroker;
        _smsSenderValidator = smsSenderValidator;
    }

    public async ValueTask<bool> SendAsync(SmsMessage smsMessage, CancellationToken cancellationToken = default)
    {
        var validationResult = _smsSenderValidator.Validate(smsMessage,
            options => options.IncludeRuleSets(NotificationEvent.OnRedering.ToString()));

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