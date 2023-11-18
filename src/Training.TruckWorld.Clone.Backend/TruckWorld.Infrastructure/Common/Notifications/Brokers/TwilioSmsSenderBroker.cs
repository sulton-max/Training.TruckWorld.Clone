using Microsoft.Extensions.Options;
using TruckWorld.Application.Common.Brokers;
using TruckWorld.Application.Common.Models;
using TruckWorld.Infrastructure.Common.Settings;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace TruckWorld.Infrastructure.Common.Notifications.Brokers;

public class TwilioSmsSenderBroker : ISmsSenderBroker
{
    private readonly TwilioSmsSenderSettings _settings;

    public TwilioSmsSenderBroker(IOptions<TwilioSmsSenderSettings> settings) =>
        _settings = settings.Value;

    public async ValueTask<bool> SendAsync(SmsMessage smsMessage, CancellationToken cancellationToken)
    {
        TwilioClient.Init(_settings.AccountSid, _settings.AuthToken);

        var messageContent = MessageResource.Create(
            body: smsMessage.Message,
            from: new PhoneNumber(_settings.SenderPhoneNumber),
            to: new PhoneNumber(smsMessage.RecieverPhoneNumber));

        return true;
    }
}