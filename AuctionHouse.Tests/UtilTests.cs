using AuctionHouse.Verifiers;

namespace AuctionHouse.Tests
{
    public class UtilTests
    {
        [Fact]
        public void ReadUint_ValidInput_ReturnsCorrectNumber()
        {
            using (new ConsoleRedirector("42\n"))
            {
                // Act
                uint result = Util.ReadUint("Enter a number:", "Invalid input");

                // Assert
                Assert.Equal((uint)42, result);
            }
        }

        [Fact]
        public void ReadUint_InvalidInputThenValid_ReturnsCorrectNumber()
        {
            using (var console = new ConsoleRedirector("abc\n42\n"))
            {
                // Act
                uint result = Util.ReadUint("Enter a number:", "Invalid input");

                // Assert
                Assert.Equal((uint)42, result);
                Assert.Contains("Invalid input", console.outputWriter.ToString());
            }
        }

        [Fact]
        public void ReadStreetNumber_ValidInput_ReturnsCorrectNumber()
        {
            using (new ConsoleRedirector("123\n"))
            {
                // Act
                uint result = Util.ReadStreetNumber("Enter street number:", "Invalid input");

                // Assert
                Assert.Equal((uint)123, result);
            }
        }

        [Fact]
        public void ReadStreetNumber_ZeroInput_ShowsErrorAndRetriesUntilValid()
        {
            using (var console = new ConsoleRedirector("0\n42\n"))
            {
                // Act
                uint result = Util.ReadStreetNumber("Enter street number:", "Invalid input");

                // Assert
                Assert.Equal((uint)42, result);
                Assert.Contains("Street number must be greater than 0", console.outputWriter.ToString());
            }
        }

        [Fact]
        public void ReadString_WithNonBlankVerifier_ValidInput_ReturnsInput()
        {
            using (new ConsoleRedirector("Test String\n"))
            {
                // Act
                string result = Util.ReadString("Enter a string:", new NonBlankStringVerifier("test"));

                // Assert
                Assert.Equal("Test String", result);
            }
        }

        [Fact]
        public void ReadString_WithNonBlankVerifier_BlankInputThenValid_ReturnsValidInput()
        {
            using (var console = new ConsoleRedirector("\n\nValid Input\n"))
            {
                // Act
                string result = Util.ReadString("Enter a string:", new NonBlankStringVerifier("test"));

                // Assert
                Assert.Equal("Valid Input", result);
                Assert.Contains("Error: test must not be blank", console.outputWriter.ToString());
            }
        }

        [Fact]
        public void ReadDateTime_ValidInput_ReturnsCorrectDateTime()
        {
            var expectedDate = new DateTime(2025, 3, 15, 14, 30, 0);
            using (new ConsoleRedirector("15/03/2025 14:30\n"))
            {
                // Create a mock verifier that always returns true
                var mockVerifier = new MockVerifier(true);

                // Act
                DateTime result = Util.ReadDateTime("Enter date and time:", mockVerifier);

                // Assert
                Assert.Equal(expectedDate, result);
            }
        }

        [Fact]
        public void ReadAddress_ValidInputs_ReturnsCompleteAddress()
        {
            // Arrange
            // Prepare a complete sequence of inputs for the address
            // Format: unitNumber, streetNumber, streetName, streetSuffix, city, state, postcode
            string input = "5\n" +        // Unit number (5)
                           "123\n" +      // Street number (123)
                           "Main\n" +     // Street name
                           "Street\n" +   // Street suffix
                           "Brisbane\n" + // City
                           "QLD\n" +      // State
                           "4000\n";      // Postcode

            using (new ConsoleRedirector(input))
            {
                // Act
                Address result = Util.ReadAddress();

                // Assert
                Assert.NotNull(result);
                Assert.Equal((uint)5, result.unitNumber);
                Assert.Equal((uint)123, result.streetNumber);
                Assert.Equal("Main", result.streetName);
                Assert.Equal("Street", result.streetSuffix);
                Assert.Equal("Brisbane", result.city);
                Assert.Equal("QLD", result.state);
                Assert.Equal((uint)4000, result.postcode);
            }
        }

        [Fact]
        public void ReadAddress_NoUnitNumber_ReturnsAddressWithoutUnit()
        {
            // Arrange
            string input = "0\n" +        // Unit number (0 = none)
                           "123\n" +      // Street number
                           "Main\n" +     // Street name
                           "Street\n" +   // Street suffix
                           "Brisbane\n" + // City
                           "QLD\n" +      // State
                           "4000\n";      // Postcode

            using (new ConsoleRedirector(input))
            {
                // Act
                Address result = Util.ReadAddress();

                // Assert
                Assert.NotNull(result);
                Assert.Equal((uint)0, result.unitNumber);
                Assert.Equal((uint)123, result.streetNumber);
                Assert.Equal("Main", result.streetName);
                Assert.Equal("Street", result.streetSuffix);
                Assert.Equal("Brisbane", result.city);
                Assert.Equal("QLD", result.state);
                Assert.Equal((uint)4000, result.postcode);
            }
        }

        [Fact]
        public void ReadAddress_InvalidThenValidState_ReturnsAddressWithValidState()
        {
            // Arrange
            string input = "0\n" +        // Unit number (0 = none)
                           "123\n" +      // Street number
                           "Main\n" +     // Street name
                           "Street\n" +   // Street suffix
                           "Brisbane\n" + // City
                           "INVALID\n" +  // Invalid state
                           "QLD\n" +      // Valid state
                           "4000\n";      // Postcode

            using (var console = new ConsoleRedirector(input))
            {
                // Act
                Address result = Util.ReadAddress();

                // Assert
                Assert.NotNull(result);
                Assert.Equal("QLD", result.state);
                Assert.Contains("Invalid state", console.outputWriter.ToString());
            }
        }

        [Fact]
        public void ReadAddress_InvalidPostcode_PromptsTillValid()
        {
            // Arrange
            string input = "0\n" +        // Unit number (0 = none)
                           "123\n" +      // Street number
                           "Main\n" +     // Street name
                           "Street\n" +   // Street suffix
                           "Brisbane\n" + // City
                           "QLD\n" +      // State
                           "999\n" +      // Invalid postcode (below 1000)
                           "10000\n" +    // Invalid postcode (above 9999)
                           "4000\n";      // Valid postcode

            using (var console = new ConsoleRedirector(input))
            {
                // Act
                Address result = Util.ReadAddress();

                // Assert
                Assert.NotNull(result);
                Assert.Equal((uint)4000, result.postcode);
                Assert.Contains("Postcode must be between an integer 1000 and 9999", console.outputWriter.ToString());
            }
        }

    }

    // Simple mock verifier for testing
    public class MockVerifier : IVerifier
    {
        private readonly bool _returnValue;

        public MockVerifier(bool returnValue)
        {
            _returnValue = returnValue;
        }

        public bool Verify(object thingToVerify)
        {
            return _returnValue;
        }
    }
}