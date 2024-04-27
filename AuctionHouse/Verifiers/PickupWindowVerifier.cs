using static System.Console;

namespace AuctionHouse.Verifiers
{
    /// <summary>
    /// Verifies if the inputted pickup window from the client is valid.
    /// </summary>
    internal class PickupWindowVerifier : IVerifier
    {
        /// <summary>
        /// The date and time for a bid's pickup window.
        /// </summary>
        private DateTime pickupWindowTime;

        /// <summary>
        /// Error message displayed to the user if pickup window is invalid.
        /// </summary>
        private string errorMessage;

        /// <summary>
        /// Create a new PickupWindowVerifier object.
        /// </summary>
        /// <param name="pickupWindowTime">The date and time for a bid's pickup window.</param>
        /// <param name="errorMessage">Error message displayed to the user if pickup window is invalid.</param>
        public PickupWindowVerifier(DateTime pickupWindowTime, string errorMessage)
        {
            this.pickupWindowTime = pickupWindowTime;
            this.errorMessage = errorMessage;
        }

        /// <summary>
        /// Verifies the inputted pickup window.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Returns true if pickup window is valid otherwise display and error message and return false.</returns>
        public bool Verify(object input)
        {
            if (DateTime.TryParse((string)input, out DateTime pickupWindow))
            {
                if (pickupWindow >= pickupWindowTime.AddHours(1))
                {
                    return true;
                }

                else
                {
                    WriteLine($"{errorMessage}");
                    return false;
                }
            }
            else
            {
                WriteLine("\tPlease enter a valid date and time.");
                return false;
            }
        }
    }
}
