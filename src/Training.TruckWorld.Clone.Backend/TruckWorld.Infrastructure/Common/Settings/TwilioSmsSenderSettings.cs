namespace TruckWorld.Infrastructure.Common.Settings;

/// <summary>
/// Represent a sending setting of sms
/// </summary>
public class TwilioSmsSenderSettings
{
    /// <summary>
    /// Gets or sets AccountSid
    /// </summary>
    public string AccountSid { get; set; } = default!;

    /// <summary>
    /// Gets or sets Authrization Token
    /// </summary>
    public string AuthToken { get; set; } = default!;

    /// <summary>
    /// Gets or sets sender's phone numbers
    /// </summary>
    public string SenderPhoneNumber { get; set; } = default!;
}