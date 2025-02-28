using static System.Console;

namespace AuctionHouse.Verifiers
{
    /// <summary>
    /// Verifies if the inputted password from the client is valid.
    /// </summary>
    internal class PasswordVerifier : IVerifier
    {
        /// <summary>
        /// Verifies the inputted password.
        /// </summary>
        /// <param name="passwordObject">Inputted password.</param>
        /// <returns>Returns true if password is valid otherwise display error message and return false.</returns>
        public bool Verify(object passwordObject)
        {
            const string error = "\tThe supplied value is not a valid password.\n";

            // Check if there is at least 8 non white space characters
            string password = (string)passwordObject;
            if (password.Where(character => !char.IsWhiteSpace(character)).Count() < 8)
            {
                WriteLine(error);
                return false;
            }

            // Check if there isn't any uppercase characters
            if (!password.Any(character => char.IsUpper(character)))
            {
                WriteLine(error);
                return false;
            }

            // Check if there isn't any lowercase characters
            if (!password.Any(character => char.IsLower(character)))
            {
                WriteLine(error);
                return false;
            }

            // Check if there isn't any digit characters
            if (!password.Any(character => char.IsDigit(character)))
            {
                WriteLine(error);
                return false;
            }

            // Check if there isn't any non-alphanumeric characters
            if (!password.Any(character => !char.IsLetterOrDigit(character)))
            {
                WriteLine(error);
                return false;
            }
            return true;

        }
    }
}
