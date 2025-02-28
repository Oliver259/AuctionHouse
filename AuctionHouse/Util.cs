using AuctionHouse.Verifiers;
using static System.Console;
using System.Runtime.CompilerServices;

// Makes Util visible to tests
[assembly: InternalsVisibleTo("AuctionHouse.Tests")]

namespace AuctionHouse
{
    /// <summary>
    /// Defines methods for utility functions.
    /// </summary>
    internal static class Util
    {
        /// <summary>
        /// Read an unsigned integer and writes a customisable error message if input is invalid.
        /// </summary>
        /// <param name="prompt">The prompt to print.</param>
        /// <returns>Returns the result if input is valid otherwise writes an error message to the client.</returns>
        internal static uint ReadUint(string prompt, string errorMessage)
        {
            while (true)
            {
                WriteLine(prompt);
                Write("> ");
                string userInput = ReadLine() ?? string.Empty;
                uint result;

                if (uint.TryParse(userInput, out result))
                {
                    return result;
                }

                WriteLine($"{errorMessage}");
            }
        }

        /// <summary>
        /// Reads a street number from client and writes a customisable error message if input is invalid.
        /// </summary>
        /// <param name="prompt">The prompt to print.</param>
        /// <param name="errorMessage">The error message to print.</param>
        /// <returns>Returns the result if input is valid otherwise writes an error message to the client.</returns>
        internal static uint ReadStreetNumber(string prompt, string errorMessage)
        {
            while (true)
            {
                WriteLine(prompt);
                Write("> ");
                string userInput = ReadLine() ?? string.Empty;
                uint result;

                if (uint.TryParse(userInput, out result))
                {
                    if (result == 0)
                    {
                        WriteLine("\tStreet number must be greater than 0.");
                        continue;
                    }

                    return result;
                }

                // Display error message
                WriteLine($"{errorMessage}");
            }
        }

        /// <summary>
        /// Read an unsigned integer and writes an error message if input is invalid.
        /// </summary>
        /// <param name="prompt">The prompt to print.</param>
        /// <returns>Returns the result if input is valid otherwise writes an error message to the client.</returns>
        internal static uint ReadUint(string prompt)
        {
            while (true)
            {
                WriteLine(prompt);
                Write("> ");
                string userInput = ReadLine() ?? string.Empty;
                uint result;

                if (uint.TryParse(userInput, out result))
                {
                    return result;
                }

                // Display error message
                WriteLine($"Error: must be a non-negative integer.");
            }
        }

        /// <summary>
        /// Read an unsigned integer, verifies it with a given verifier and writes an error message if input is invalid.
        /// </summary>
        /// <param name="prompt">The prompt to print</param>
        /// <param name="verifier">Verifier used</param>
        /// <returns>Returns the result if input is valid otherwise writes an error message to the client.</returns>
        internal static uint ReadUint(string prompt, IVerifier verifier)
        {
            while (true)
            {
                WriteLine(prompt);
                Write("> ");
                string userInput = ReadLine() ?? string.Empty;
                uint result;

                if (uint.TryParse(userInput, out result))
                {
                    if (verifier.Verify(result))
                    {
                        return result;
                    }
                    else
                    {
                        continue;
                    }
                }

                // Display error message
                WriteLine("Error: you must enter a positive integer");
            }
        }

        /// <summary>
        /// Reads a string and verifies it with a given verifier.
        /// </summary>
        /// <param name="prompt">The prompt to print.</param>
        /// <param name="verifier">Verifier used</param>
        /// <returns>Returns the userInput if input is valid.</returns>
        internal static string ReadString(string prompt, IVerifier verifier)
        {
            while (true)
            {
                WriteLine(prompt);
                Write("> ");
                string userInput = ReadLine() ?? string.Empty;

                // If input is valid then return input
                if (verifier.Verify(userInput))
                {
                    return userInput;
                }
            }
        }

        /// <summary>
        /// Reads a date and time and verifies it with a given verifier.
        /// </summary>
        /// <param name="prompt">The prompt to print.</param>
        /// <param name="verifier">Verifier used.</param>
        /// <returns>Returns the userInput if input is valid.</returns>
        internal static DateTime ReadDateTime(string prompt, IVerifier verifier)
        {
            while (true)
            {
                WriteLine(prompt);
                Write("> ");
                string userInput = ReadLine() ?? string.Empty;

                if (verifier.Verify(userInput))
                {
                    return DateTime.Parse(userInput);
                }
            }
        }

        /// <summary>
        /// Prompts the client for their addess informaiton, reads it and then stores it in Address class.
        /// </summary>
        /// <returns>Returns a newly created address if all input is valid.</returns>
        internal static Address ReadAddress()
        {
            // Prompt the client for their unit number and store it
            uint unitNumberInput = Util.ReadUint("\nUnit number (0 = none):", "\tUnit number must be a non-negative integer.");
            uint streetNumberInput = Util.ReadStreetNumber("\nStreet number:", "\tStreet number must be a positive integer.");
            string streetNameInput = Util.ReadString("\nStreet name:", new NonBlankStringVerifier("street name"));
            string streetSuffixInput = Util.ReadString("\nStreet suffix:", new NonBlankStringVerifier("street suffix"));
            string cityInput = Util.ReadString("\nCity:", new NonBlankStringVerifier("city"));
            WriteLine();

            string stateInput;
            while (true)
            {
                // Prompt the client for their state
                WriteLine("State (ACT, NSW, NT, QLD, SA, TAS, VIC, WA):");
                Write("> ");
                stateInput = ReadLine();
                stateInput = stateInput.ToUpper();

                // Check if state is valid
                if (stateInput == "QLD" || stateInput == "NSW" || stateInput == "VIC" || stateInput == "TAS" || stateInput == "SA" || stateInput == "WA" || stateInput == "NT" || stateInput == "ACT")
                {
                    WriteLine();
                    break;
                }

                WriteLine("\tInvalid state\n");
            }
            uint postcodeInput = Util.ReadUint("Postcode (1000 .. 9999):", new NumberRangeVerifier(1000, 9999, "\tPostcode must be between an integer 1000 and 9999\n"));

            return new Address(unitNumberInput, streetNumberInput, streetNameInput, streetSuffixInput, cityInput, postcodeInput, stateInput);
        }
    }
}
