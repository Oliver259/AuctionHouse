namespace AuctionHouse.Verifiers
{
    /// <summary>
    /// Verifies the inputted bid confirmation from the client.
    /// </summary>
    internal class BidConfirmationVerifier : IVerifier
    {
        /// <summary>
        /// Verifies the user's input.
        /// </summary>
        /// <param name="bidConfirmation">Inputted bid confirmation.</param>
        /// <returns>Returns true if bid confirmation is either yes or no otherwise if input is invalid display an error message and return false.</returns>
        public bool Verify(object bidConfirmation)
        {
            if ((string)bidConfirmation == "no" || (string)bidConfirmation == "yes")
            {
                return true;
            }

            else
            {
                Console.WriteLine("Error: you must input yes or no");
            }
            return false;
        }
    }
}
