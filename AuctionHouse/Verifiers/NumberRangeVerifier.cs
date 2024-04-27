using static System.Console;

namespace AuctionHouse.Verifiers
{
    /// <summary>
    /// Verifies if the number inputted is between 2 numbers.
    /// </summary>
    internal class NumberRangeVerifier : IVerifier
    {
        /// <summary>
        /// The minimum number.
        /// </summary>
        int minimum;

        /// <summary>
        /// The maximum number.
        /// </summary>
        int maximum;

        /// <summary>
        /// Error message to be displayed if a number inputted isn't between the minimum and maximum range.
        /// </summary>
        string errorMessage;

        /// <summary>
        /// Creates a new NumberRangeVerifier object.
        /// </summary>
        /// <param name="minimum">The minimum number.</param>
        /// <param name="maximum">The maximum number.</param>
        /// <param name="errorMessage">Error message to be displayed if a number inputted isn't between the minimum and maximum range.</param>
        public NumberRangeVerifier(int minimum, int maximum, string errorMessage)
        {
            this.minimum = minimum;
            this.maximum = maximum;
            this.errorMessage = errorMessage;

        }

        /// <summary>
        /// Verifies if the inputted number is between the minimum and maximum.
        /// </summary>
        /// <param name="numberObject"></param>
        /// <returns>Returns true if the inputted number is between the minimum and maximum value otherwise display an error message and return false.</returns>
        public bool Verify(object numberObject)
        {


            uint number = (uint)numberObject;
            if (number >= minimum && number <= maximum)
            {
                return true;
            }

            else
            {
                WriteLine($"{errorMessage}");
                return false;
            }
        }
    }
}
