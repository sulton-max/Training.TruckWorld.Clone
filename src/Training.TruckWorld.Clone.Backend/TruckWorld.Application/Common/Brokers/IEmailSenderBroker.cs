using TruckWorld.Application.Common.Models;

namespace TruckWorld.Application.Common.Brokers;

public interface IEmailSenderBroker
{
    ValueTask<bool> SendAsync(EmailMessage emailMessage, CancellationToken cancellationToken);
}