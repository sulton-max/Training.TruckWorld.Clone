using TruckWorld.Application.Common.Notificaitons.Models;

namespace TruckWorld.Application.Common.Notificaitons.Services;

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