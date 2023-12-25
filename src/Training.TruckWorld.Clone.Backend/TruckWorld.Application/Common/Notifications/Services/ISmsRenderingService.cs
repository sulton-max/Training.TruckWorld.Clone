﻿using TruckWorld.Application.Common.Notifications.Models;

namespace TruckWorld.Application.Common.Notifications.Services;

/// <summary>
/// Represents a service for rendering sms message
/// </summary>
public interface ISmsRenderingService
{

    /// <summary>
    /// Asynchronously renders sms messages
    /// </summary>
    /// <param name="smsMessage"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<string> RenderAsync(
        SmsMessage smsMessage,
        CancellationToken cancellationToken = default
    );
}