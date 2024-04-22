using System.Text.RegularExpressions;

namespace PhoneNumberDetector;

public partial class TenDigitStrategy : IPhoneNumberParsingStrategy
{
    public bool CanHandle(string input)
    {
        return TenDigitPhoneNumber().IsMatch(input);
    }

    public PhoneNumber Parse(string input)
    {
        // Remove all non-digit characters
        var cleanedInput = NonDigitCharacters().Replace(input, "");
        
        return new PhoneNumber(NormalizedNumber: input, Format: "10-digit", AreaCode: cleanedInput[..3],
            Number: cleanedInput[3..], CountryCode: null);
    }

    [GeneratedRegex(@"^\d{3}[-\s]?\d{3}[-\s]?\d{4}$")]
    private static partial Regex TenDigitPhoneNumber();
    
    [GeneratedRegex(@"\D")]
    private static partial Regex NonDigitCharacters();
}