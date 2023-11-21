using TruckWorld.Application.Common.Models;

namespace TruckWorld.Application.Common.Brokers;

public interface ISmsSenderBroker
{
    ValueTask<bool> SendAsync(SmsMessage smsMessage, CancellationToken cancellationToken = default);
}