using TruckWorld.Application.Common.Models;

namespace TruckWorld.Application.Common.Services;
/// <summary>
/// Represent sending sms messages
/// </summary>
public interface ISmsSenderService
{
    /// <summary>
    /// Function that sends sms messages
    /// </summary>
    /// <param name="smsMessage"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<bool> SendAsync(SmsMessage smsMessage, CancellationToken cancellationToken = default);
}