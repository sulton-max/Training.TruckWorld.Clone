using TruckWorld.Application.Common.Models;

namespace TruckWorld.Application.Common.Services;

public interface ISmsSenderService
{
    ValueTask<bool> SendAsync(SmsMessage smsMessage, CancellationToken cancellationToken = default);
}