using System.Text.RegularExpressions;

namespace AuctionHouse.Verifiers
{
    /// <summary>
    /// Verifies the inputted currency amount by the client.
    /// </summary>
    internal class CurrencyVerifier : IVerifier
    {
        /// <summary>
        /// Verifies the inputted currency.
        /// </summary>
        /// <param name="currency">Inputted currency.</param>
        /// <returns>Returns true if currency is valid otherwise displays an error message and returns false.</returns>
        public bool Verify(object currency)
        {
            string pattern = "^([$]\\d+\\.\\d{2})$";
            Regex rg = new Regex(pattern);
            if (!rg.IsMatch((string)currency))
            {
                Console.WriteLine("Error: Please enter a valid product price");
                return false;
            }
            return true;
        }
    }
}
