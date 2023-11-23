namespace TruckWorld.Infrastructure.Common.Settings;

/// <summary>
/// Represent a sending setting of email
/// </summary>
public class SmtpEmailSenderSettings
{
    /// <summary>
    /// Gets or sets host
    /// </summary>
    public string Host { get; set; } = default!;

    /// <summary>
    /// Gets or sets port
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// Gets or sets a sender's address
    /// </summary>
    public string CredentialAddress { get; set; } = default!;

    /// <summary>
    /// Gets or sets a sender's email password
    /// </summary>
    public string Password { get; set; } = default!;
}