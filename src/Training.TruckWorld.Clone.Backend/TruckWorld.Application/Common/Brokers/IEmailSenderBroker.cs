using TruckWorld.Application.Common.Models;

namespace TruckWorld.Application.Common.Brokers;

/// <summary>
/// Represent sending email messages
/// </summary>
public interface IEmailSenderBroker
{
    /// <summary>
    /// Function that sends email messages
    /// </summary>
    /// <param name="emailMessage"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<bool> SendAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default);
}