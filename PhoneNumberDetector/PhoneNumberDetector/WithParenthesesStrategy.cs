using System.Text.RegularExpressions;

namespace PhoneNumberDetector;

public partial class WithParenthesesStrategy : IPhoneNumberParsingStrategy
{
    public bool CanHandle(string input)
    {
        return WithParenthesesPhoneNumber().IsMatch(input);
    }

    public PhoneNumber Parse(string input)
    {
        // Remove all non-digit characters
        var cleanedInput = NonDigitCharacters().Replace(input, "");

        var areaCode = cleanedInput.Substring(cleanedInput.Length - 10, 3);

        var countryCode = cleanedInput.Length > 10 ? cleanedInput[..^10] : null;

        return new PhoneNumber(NormalizedNumber: input, Format: "WithParentheses", AreaCode: areaCode,
            Number: cleanedInput[^7..], CountryCode: countryCode);
    }

    [GeneratedRegex(@"^((\+\d{1,3}|00\d{1,3}|[1-9]\d{0,2})?(?:( |-)?\(\d{3}\))( |-)?(\d{7})|\((\+\d{1,3}|00\d{1,3}|[1-9]\d{0,2})\)( |-)?(\d{3})(\s?|-)(\d{7}))$")]
    private static partial Regex WithParenthesesPhoneNumber();
    
    [GeneratedRegex(@"\D")]
    private static partial Regex NonDigitCharacters();
}