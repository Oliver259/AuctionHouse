using AuctionHouse.Verifiers;
using static System.Console;

namespace AuctionHouse.Dialogs
{
    /// <summary>
    /// Asks the user a name, email address and password and adds the client to the list of clients if input is valid.
    /// </summary>
    public class RegistrationDialog : Dialog
    {

        /// <summary>
        /// Initialise Dialog.
        /// </summary>
        /// <param name="auctionHouse">Auction House where products are sold.</param>
        public RegistrationDialog(AuctionHouse auctionHouse) : base(auctionHouse)
        {
        }

        public override void Display()
        {
            WriteLine("");
            WriteLine("Registration");
            WriteLine("------------\n");

            // Prompt the client for their name, verify that the name input is a non-blank text string and then store it if it is
            string name = Util.ReadString("Please enter your name", new NonBlankStringVerifier("name"));

            // Get email from client and verify it
            EmailAddressVerifier emailAddressVerifier = new EmailAddressVerifier(AuctionHouse.clients);
            string emailAddress = Util.ReadString("\nPlease enter your email address", emailAddressVerifier);

            WriteLine("");

            // Prompt the user for a password and specify required details
            string passwordPrompt = "Please choose a password\n" +
                "* At least 8 characters\n" +
                "* No white space characters\n" +
                "* At least one upper-case letter\n" +
                "* At least one lower-case letter\n" +
                "* At least one digit\n" +
                "* At least one special character";

            // Get password from client and verify it
            PasswordVerifier passwordVerifier = new PasswordVerifier();
            string password = Util.ReadString(passwordPrompt, passwordVerifier);

            // Remove all leading and trailing white-space characters from the client's password
            password = password.Trim();
            WriteLine("");

            RegisterClient(name, emailAddress, password);

            // Display a successful registration message to the client
            WriteLine($"Client {name}({emailAddress}) has successfully registered at the Auction House.");
        }

        /// <summary>
        /// Register a client to the Auction House.
        /// </summary>
        /// <param name="name">Client's name.</param>
        /// <param name="emailAddress">Client's email address.</param>
        /// <param name="password">Client's password.</param>
        private void RegisterClient(string name, string emailAddress, string password)
        {
            Client client = new Client(name, emailAddress, password);
            AuctionHouse.clients.Add(client);
            AuctionHouse.SaveClients();
        }
    }
}
