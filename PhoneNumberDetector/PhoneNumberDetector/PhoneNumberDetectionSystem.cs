using System;
using System.Collections.Generic;
using System.IO;

namespace PhoneNumberDetector;

public class PhoneNumberDetectionSystem
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Please select your input method:");
        Console.WriteLine("1. File");
        Console.WriteLine("2. Command switches");
        var inputMethod = Console.ReadLine();

        switch (inputMethod)
        {
            case "1":
                HandleFileInput();
                break;
            case "2":
                HandleCommandSwitches();
                break;
            default:
                Console.WriteLine("Invalid input method selected.");
                break;
        }
    }
    
    private static void HandleFileInput()
    {
        Console.WriteLine("Please enter the file path:");
        var filePath = Console.ReadLine();

        if (!File.Exists(filePath))
        {
            Console.WriteLine("File does not exist.");
            return;
        }

        var fileContent = File.ReadAllText(filePath);
        
        var testNumbers = fileContent.Split(',');
        
        var result = ProcessNumbers(testNumbers);
        
        // print all results
        foreach (var phoneNumber in result)
        {
            Console.WriteLine(phoneNumber);
        }
    }

    private static void HandleCommandSwitches()
    {
        Console.WriteLine("Please enter the phone numbers (separated by comma):");
        var input = Console.ReadLine();

        var testNumbers = input?.Split(',');

        if (testNumbers == null || testNumbers.Length == 0)
            Console.WriteLine("No phone numbers provided.");

        var result = ProcessNumbers(testNumbers);
        
        // print all results
        foreach (var phoneNumber in result)
        {
            Console.WriteLine(phoneNumber);
        }
    }
    
    public static List<PhoneNumber> ProcessNumbers(string[] testNumbers)
    {
        // initialize the strategies and the factory
        var strategies = new List<IPhoneNumberParsingStrategy>
        {
            new TenDigitStrategy(),
            new WithCountryCodeStrategy(),
            new WithParenthesesStrategy()
        };

        var strategyFactory = new PhoneNumberParsingStrategyFactory(strategies);

        var parsedNumbers = new List<PhoneNumber>();
        
        var normalizer = new PhoneNumberNormalizer();
        foreach (var number in testNumbers)
        {
            var normalizedNumber = normalizer.Normalize(number);
            var strategy = strategyFactory.GetStrategy(normalizedNumber);
            var parsedNumber = strategy.Parse(normalizedNumber);
            
            parsedNumbers.Add(parsedNumber);
        }

        return parsedNumbers;

    }
}
