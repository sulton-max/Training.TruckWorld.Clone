using TruckWorld.Application.Common.Notificaitons.Models;

namespace TruckWorld.Application.Common.Notificaitons.Brokers;

/// <summary>
/// Provides methods to send SMS messages
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