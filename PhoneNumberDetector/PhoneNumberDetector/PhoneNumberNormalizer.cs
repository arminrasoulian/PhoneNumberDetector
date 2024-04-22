using System.Collections.Generic;

namespace PhoneNumberDetector;

public class PhoneNumberNormalizer
{
    private static readonly Dictionary<string, string> NumberWordsMap = new()
    {
        { "ONE", "1" }, { "TWO", "2" }, { "THREE", "3" }, { "FOUR", "4" }, { "FIVE", "5" },
        { "SIX", "6" }, { "SEVEN", "7" }, { "EIGHT", "8" }, { "NINE", "9" }, { "ZERO", "0" },
        { "एक", "1" }, { "दो", "2" }, { "तीन", "3" }, { "चार", "4" }, { "पांच", "5" },
        { "छह", "6" }, { "सात", "7" }, { "आठ", "8" }, { "नौ", "9" }, { "शूɊ", "0" }
    };

    public string Normalize(string input)
    {
        var normalizedInput = input;
        
        // With the above mapping we can replace the words with numbers
        foreach (var (word, number) in NumberWordsMap)
        {
            normalizedInput = normalizedInput.Replace(word, number);
        }
        
        // Remove any spaces
        normalizedInput = normalizedInput.Replace(" ", "");

        return normalizedInput;
    }
}