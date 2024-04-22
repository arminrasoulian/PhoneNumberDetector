namespace PhoneNumberDetector;

public interface IPhoneNumberParsingStrategy
{
    PhoneNumber Parse(string input);
    bool CanHandle(string input);
}