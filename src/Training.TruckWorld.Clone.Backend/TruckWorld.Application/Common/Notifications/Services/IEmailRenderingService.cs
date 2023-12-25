using TruckWorld.Application.Common.Notifications.Models;

namespace TruckWorld.Application.Common.Notifications.Services;

/// <summary>
/// Represents a service for rendering email message
/// </summary>
public interface IEmailRenderingService
{
    /// <summary>
    /// Asynchronously renders email messages
    /// </summary>
    /// <param name="emailMessage"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<string> RenderAsync(
        EmailMessage emailMessage,
        CancellationToken cancellationToken = default
    );
}
