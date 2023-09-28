using System.Runtime.CompilerServices;

namespace Training.TruckWorld.Backend.Domain.Extensions;

public static class StringExtensions
{
    public static string ToCapitalized(this string str)
    {
        return string.Concat(
            str.Substring(0, 1).ToUpper(),
            str.Substring(1).ToLower());
    }
}
