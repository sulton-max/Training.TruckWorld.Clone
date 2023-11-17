using Microsoft.Extensions.Configuration;

namespace TruckWorld.Application.Common.Settings;

/// <summary>
/// Represents settings for user data validation.
/// </summary>
public class ValidationSettings
{
    public string EmailRegexPattern { get; set; } = default!;

    public string PasswordRegexPattern { get; set; } = default!;
}
