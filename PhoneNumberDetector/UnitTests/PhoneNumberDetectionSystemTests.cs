using PhoneNumberDetector;

namespace UnitTests;

[TestFixture]
public class PhoneNumberDetectionSystemTests
{
    [Test]
    public void ProcessNumbers_GivenValidNumbers_ShouldNotThrowException()
    {
        // Arrange
        var testNumbers = new string[] { "1234567890", "+11234567890", "(123)4567890", "+91-1234567890", "(ONE TWO THREE) FOUR FIVE SIX SEVEN EIGHT NINE ZERO" };

        var result = PhoneNumberDetectionSystem.ProcessNumbers(testNumbers);

        // Assuming that the expected output is a list of PhoneNumber objects
        var expectedOutput = new List<PhoneNumber>
        {
            new(NormalizedNumber: "1234567890", Format: "10-digit", AreaCode: "123", Number: "4567890", CountryCode: null),
            new(NormalizedNumber: "+11234567890", Format: "WithCountryCode", AreaCode: "123", Number: "4567890", CountryCode: "1"),
            new(NormalizedNumber: "(123)4567890", Format: "WithParentheses", AreaCode: "123", Number: "4567890", CountryCode: null),
            new(NormalizedNumber: "+91-1234567890", Format: "WithCountryCode", AreaCode: "123", Number: "4567890", CountryCode: "91"),
            new(NormalizedNumber: "(123)4567890", Format: "WithParentheses", AreaCode: "123", Number: "4567890", CountryCode: null),
        };

        // Assert result and expectedOutput are equal
        Assert.That(result, Is.EqualTo(expectedOutput));
    }
}