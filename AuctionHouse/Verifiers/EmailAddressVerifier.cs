using System.Text.RegularExpressions;
using static System.Console;

namespace AuctionHouse.Verifiers
{
    /// <summary>
    /// Verifies the inputted email address from the client.
    /// </summary>
    internal class EmailAddressVerifier : IVerifier
    {
        /// <summary>
        /// List of clients.
        /// </summary>
        private List<Client> clients;

        /// <summary>
        /// Create a new EmailAddressVerifier object.
        /// </summary>
        /// <param name="clients">List of clients.</param>
        public EmailAddressVerifier(List<Client> clients)
        {
            this.clients = clients;
        }

        /// <summary>
        /// Verifies the inputted email address.
        /// </summary>
        /// <param name="emailAddress">Inputted email address.</param>
        /// <returns>Returns true if password is valid otherwise displays an error message and returns false or if the email address is already in use it displays an error message and then returns false.</returns>
        public bool Verify(object emailAddress)
        {
            string pattern = "^[\\w\\-\\.]+(?<![\\.\\-_])@(?:[\\w\\-]+\\.[\\w\\-]+)*[\\w\\-]+\\.[a-zA-Z]+$";
            Regex rg = new Regex(pattern);
            if (!rg.IsMatch((string)emailAddress))
            {
                WriteLine("\tThe supplied value is not a valid email address.\n");
                return false;
            }

            foreach (Client client in clients)
            {
                if (client.emailAddress.Equals(emailAddress))
                {
                    WriteLine("\tThe supplied address is already in use.\n");
                    return false;
                }
            }

            return true;
        }
    }

}
