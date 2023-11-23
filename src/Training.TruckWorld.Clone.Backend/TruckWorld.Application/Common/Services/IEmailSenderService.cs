using TruckWorld.Application.Common.Models;

namespace TruckWorld.Application.Common.Services;

/// <summary>
/// Represent sending email messages
/// </summary>
public interface IEmailSenderService
{
    /// <summary>
    /// Function that sends email messages
    /// </summary>
    /// <param name="emailMessage"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<bool> SendAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default);
}