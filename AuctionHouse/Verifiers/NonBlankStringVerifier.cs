using static System.Console;

namespace AuctionHouse.Verifiers
{
    /// <summary>
    /// Verifies if the input given from the client is a non-blank string.
    /// </summary>
    internal class NonBlankStringVerifier : IVerifier
    {
        /// <summary>
        /// The type of input.
        /// </summary>
        string inputType;

        /// <summary>
        /// Create a new NonBlankStringVerifier object.
        /// </summary>
        /// <param name="inputType">The type of input.</param>
        public NonBlankStringVerifier(string inputType)
        {
            this.inputType = inputType;
        }

        /// <summary>
        /// Verifies the user's input.
        /// </summary>
        /// <param name="inputObject">User input.</param>
        /// <returns>Returns true if user input is valid otherwise display an error message and return false.</returns>
        public bool Verify(object inputObject)

        {
            // Check if the input isn't a non-blank string
            string input = (string)inputObject;
            if (string.IsNullOrEmpty(input))
            {
                WriteLine($"Error: {inputType} must not be blank");
                return false;
            }
            return true;
        }
    }
}
