using TruckWorld.Application.Common.Models;

namespace TruckWorld.Application.Common.Services;

public interface IEmailSenderService
{
    ValueTask<bool> SendAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default);
}