namespace TruckWorld.Application.Common.Settings;

/// <summary>
/// Represents settings for user data validation.
/// </summary>
public class ValidationSettings
{
    /// <summary>
    /// Gets or sets the regular expression pattern for validating email addresses.
    /// </summary>
    public string EmailRegexPattern { get; set; } = default!;

    /// <summary>
    /// Gets or sets the regular expression pattern for validating passwords.
    /// </summary>
    public string PasswordRegexPattern { get; set; } = default!;

    /// <summary>
    /// Gets or sets the duration, in seconds, for which a verification code remains valid.
    /// </summary>
    public int VerificationCodeExpiryTimeInSeconds { get; set; } = default!;

    /// <summary>
    /// Gets or sets the regular expression pattern used for URL validation.
    /// </summary>
    public string UrlRegexPattern { get; set; } = default!;
}
