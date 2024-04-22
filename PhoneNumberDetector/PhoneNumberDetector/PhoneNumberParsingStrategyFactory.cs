using System;
using System.Collections.Generic;

namespace PhoneNumberDetector;

public class PhoneNumberParsingStrategyFactory(IList<IPhoneNumberParsingStrategy> strategies)
{
    public IPhoneNumberParsingStrategy GetStrategy(string input)
    {
        foreach (var strategy in strategies)
        {
            if (strategy.CanHandle(input))
            {
                return strategy;
            }
        }

        throw new ArgumentException("No strategy found to handle the input.");
    }
}