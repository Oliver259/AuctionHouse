using System.Globalization;
using AuctionHouse.Verifiers;

namespace AuctionHouse.Tests
{
    public class VerifierTests
    {
        [Fact]
        public void EmailAddressVerifier_AcceptsValidNonDuplicateEmail()
        {
            // Arrange
            using var console = new ConsoleRedirector("");
            var clients = new List<Client> { 
                new Client("Test1", "existing1@example.com", "Password123!"),
                new Client("Test2", "existing2@example.com", "Password123!")
            };
            var verifier = new EmailAddressVerifier(clients);
    
            // Act
            bool result = verifier.Verify("new@example.com");
    
            // Assert
            Assert.True(result);
            // Output should be empty since no errors occurred
            Assert.Equal("", console.outputWriter.ToString().Trim());
        }
        
        [Theory]
        [InlineData("test@example.com", true)]
        [InlineData("invalid-email", false)]
        [InlineData("missing@dot", false)]
        [InlineData("@missingprefix.com", false)]
        [InlineData("ending.with.dot.@example.com", false)]
        [InlineData("valid-email_123@sub.domain.com", true)]
        public void EmailAddressVerifier_ValidatesEmailsCorrectly(string email, bool expectedResult)
        {
            // Arrange
            using var console = new ConsoleRedirector("");
            var verifier = new EmailAddressVerifier(new List<Client>());
    
            // Act
            bool result = verifier.Verify(email);
    
            // Assert - only check the validation logic result
            Assert.Equal(expectedResult, result);
        }
        
        [Fact]
        public void EmailAddressVerifier_RejectsExistingEmail()
        {
            // Arrange
            using var console = new ConsoleRedirector("");
            var clients = new List<Client> { new Client("Test", "existing@example.com", "Password123!") };
            var verifier = new EmailAddressVerifier(clients);
            
            // Act
            bool result = verifier.Verify("existing@example.com");
            
            // Assert
            Assert.False(result);
            Assert.Contains("already in use", console.outputWriter.ToString().ToLower());
        }
        
        [Theory]
        [InlineData("Password1!", true)]
        [InlineData("password1!", false)] // No uppercase
        [InlineData("PASSWORD1!", false)] // No lowercase
        [InlineData("Password!", false)]  // No digit
        [InlineData("Password1", false)]  // No special character
        [InlineData("Pass1!", false)]     // Too short
        public void PasswordVerifier_ValidatesPasswordsCorrectly(string password, bool expectedResult)
        {
            // Arrange
            using var console = new ConsoleRedirector("");
            var verifier = new PasswordVerifier();
            
            // Act
            bool result = verifier.Verify(password);
            
            // Assert
            Assert.Equal(expectedResult, result);
        }
        
        [Theory]
        [InlineData("$100.00", true)]
        [InlineData("100.00", false)]
        [InlineData("$100", false)]
        [InlineData("$100.0", false)]
        [InlineData("$100.000", false)]
        public void CurrencyVerifier_ValidatesCurrencyCorrectly(string currency, bool expectedResult)
        {
            // Arrange
            using var console = new ConsoleRedirector("");
            var verifier = new CurrencyVerifier();
            
            // Act
            bool result = verifier.Verify(currency);
            
            // Assert
            Assert.Equal(expectedResult, result);
        }
        
        [Theory]
        [InlineData("yes", true)]
        [InlineData("no", true)]
        [InlineData("maybe", false)]
        [InlineData("", false)]
        public void BidConfirmationVerifier_ValidatesConfirmationCorrectly(string confirmation, bool expectedResult)
        {
            // Arrange
            using var console = new ConsoleRedirector("");
            var verifier = new BidConfirmationVerifier();
            
            // Act
            bool result = verifier.Verify(confirmation);
            
            // Assert
            Assert.Equal(expectedResult, result);
        }
        
        [Theory]
        [InlineData("Test", true)]
        [InlineData("", false)]
        [InlineData("   ", false)]
        public void NonBlankStringVerifier_ValidatesStringCorrectly(string input, bool expectedResult)
        {
            // Arrange
            using var console = new ConsoleRedirector("");
            var verifier = new NonBlankStringVerifier("test field");
            
            // Act
            bool result = verifier.Verify(input);
            
            // Assert
            Assert.Equal(expectedResult, result);
        }
        
        [Theory]
        [InlineData(5, true)]  // Within range
        [InlineData(1, true)]  // At minimum
        [InlineData(10, true)] // At maximum
        [InlineData(0, false)] // Below minimum
        [InlineData(11, false)] // Above maximum
        public void NumberRangeVerifier_ValidatesRangeCorrectly(uint number, bool expectedResult)
        {
            // Arrange
            using var console = new ConsoleRedirector("");
            var verifier = new NumberRangeVerifier(1, 10, "Number must be between 1 and 10");
            
            // Act
            bool result = verifier.Verify(number);
            
            // Assert
            Assert.Equal(expectedResult, result);
        }
        
        [Fact]
        public void PickupWindowVerifier_AcceptsValidFutureTime()
        {
            // Arrange
            using var console = new ConsoleRedirector("");
            var currentTime = DateTime.Now;
            var verifier = new PickupWindowVerifier(currentTime, "Error message");
            var futureTime = currentTime.AddHours(2).ToString(CultureInfo.InvariantCulture);
            
            // Act
            bool result = verifier.Verify(futureTime);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void PickupWindowVerifier_RejectsTooEarlyTime()
        {
            // Arrange
            using var console = new ConsoleRedirector("");
            var currentTime = DateTime.Now;
            var verifier = new PickupWindowVerifier(currentTime, "Time must be at least 1 hour in the future");
            var earlyTime = currentTime.AddMinutes(30).ToString(CultureInfo.InvariantCulture);
            
            // Act
            bool result = verifier.Verify(earlyTime);
            
            // Assert
            Assert.False(result);
            Assert.Contains("Time must be at least 1 hour in the future", console.outputWriter.ToString());
        }
        
        [Fact]
        public void PickupWindowVerifier_RejectsInvalidDateTimeFormat()
        {
            // Arrange
            using var console = new ConsoleRedirector("");
            var currentTime = DateTime.Now;
            var verifier = new PickupWindowVerifier(currentTime, "Time must be at least 1 hour in the future");
            var invalidDateTime = "not-a-date-time";
    
            // Act
            bool result = verifier.Verify(invalidDateTime);
    
            // Assert
            Assert.False(result);
            Assert.Contains("Please enter a valid date and time", console.outputWriter.ToString());
        }
    }
}