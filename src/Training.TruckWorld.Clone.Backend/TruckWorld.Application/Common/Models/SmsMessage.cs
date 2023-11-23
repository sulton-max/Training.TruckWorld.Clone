﻿using TruckWorld.Domain.Entities;

namespace TruckWorld.Application.Common.Models;

/// <summary>
/// Represents a sms message
/// </summary>
public class SmsMessage : NotificationMessage
{
    /// <summary>
    /// Gets or sets sender phone numbers
    /// </summary>
    public string SenderPhoneNumber { get; set; } = default!;

    /// <summary>
    /// Gets or sets reciever phone numbers
    /// </summary>
    public string RecieverPhoneNumber { get; set; } = default!;

    /// <summary>
    /// Gets or sets SmsTemplate entities
    /// </summary>
    public SmsTemplate Template { get; set; } = default!;

    /// <summary>
    /// Gets or sets the sms messages
    /// </summary>
    public string Message { get; set; } = default!;
}