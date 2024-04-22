using System.Text.RegularExpressions;

namespace PhoneNumberDetector;

public partial class WithCountryCodeStrategy : IPhoneNumberParsingStrategy
{
    public bool CanHandle(string input)
    {
        return WithCountryCodePhoneNumber().IsMatch(input);
    }

    public PhoneNumber Parse(string input)
    {
        // Remove all non-digit characters
        var cleanedInput = NonDigitCharacters().Replace(input, "");
        
        var areaCode = cleanedInput.Substring(cleanedInput.Length - 10, 3);

        var countryCode = cleanedInput.Length > 10 ? cleanedInput[..^10] : null;

        return new PhoneNumber(NormalizedNumber: input, Format: "WithCountryCode", AreaCode: areaCode,
            Number: cleanedInput[^7..], CountryCode: countryCode);
    }

    [GeneratedRegex(@"^(\+\d{1,3}|00\d{1,3}|[1-9]\d{0,2})( |-)?(\d{3})( |-)?(\d{3})( |-)?\d{4}$")]
    private static partial Regex WithCountryCodePhoneNumber();
    
    [GeneratedRegex(@"\D")]
    private static partial Regex NonDigitCharacters();
}