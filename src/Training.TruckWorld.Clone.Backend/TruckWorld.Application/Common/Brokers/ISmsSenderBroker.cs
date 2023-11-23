using TruckWorld.Application.Common.Models;

namespace TruckWorld.Application.Common.Brokers;

/// <summary>
/// Represent sending sms messages
/// </summary>
public interface ISmsSenderBroker
{
    /// <summary>
    /// Function that sends sms messages
    /// </summary>
    /// <param name="smsMessage"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<bool> SendAsync(SmsMessage smsMessage, CancellationToken cancellationToken = default);
}