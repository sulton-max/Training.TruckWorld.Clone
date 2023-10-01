using System.Globalization;

namespace Training.TruckWorld.Backend.Infrastructure.Accounts.Models;

public class LoginDetails
{
    public string EmailAddress { get; set; }
    public string Password { get; set; }
}