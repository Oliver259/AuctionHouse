using AuctionHouse.Dialogs.Menus;
using static System.Console;

namespace AuctionHouse.Dialogs
{
    /// <summary>
    /// Asks the user to sign in. If the input is valid it signs them in, if the client has not signed in before it asks the client to provide a valid address.
    /// </summary>
    public class SiginDialog : Dialog
    {
        /// <summary>
        /// Initialise Dialog.
        /// </summary>
        /// <param name="auctionHouse">Auction House where products are sold.</param>
        public SiginDialog(AuctionHouse auctionHouse) : base(auctionHouse)
        {
        }

        public override void Display()
        {
            AuctionHouse.AddHeading("\nSign In");
            // Prompt the client for their name
            WriteLine("\nPlease enter your email address");

            Write("> ");
            // Get name from client
            string emailAddress = ReadLine();

            WriteLine("");
            WriteLine("Please enter your password");

            Write("> ");
            // Get password from client
            string password = ReadLine();

            SigninClient(emailAddress, password);

        }

        /// <summary>
        /// Prompts the user for an address and set client.hasSignedIn to true so the client will not need to enter an address again.
        /// </summary>
        /// <param name="client"></param>
        private void FirstSignIn(Client client)
        {
            Address homeAddress = Util.ReadAddress();

            WriteLine($"\nAddress has been updated to {homeAddress}");

            client.homeAddress = homeAddress;

            client.hasSignedIn = true;
            AuctionHouse.SaveClients();

        }

        /// <summary>
        /// Checks if the inputted email and password match a existing client and signs in the client if it does otherwise display error message.
        /// </summary>
        /// <param name="emailAddress">Client's email address.</param>
        /// <param name="password">Client's password.</param>
        private void SigninClient(string emailAddress, string password)
        {
            foreach (var client in AuctionHouse.clients)
            {
                if (client.emailAddress == emailAddress && client.password == password)
                {
                    AuctionHouse.loggedInClient = client;

                    if (client.hasSignedIn == false)
                    {

                        AuctionHouse.AddHeading($"\nPersonal Details for {AuctionHouse.loggedInClient.Name}({emailAddress})");

                        WriteLine("\nPlease provide your home address.");

                        FirstSignIn(client);
                        // Make it display client menu
                        ClientMenuDialog clientMenu = new ClientMenuDialog(AuctionHouse);
                        clientMenu.Display();
                        return;
                    }
                    else
                    {
                        // Make it display client menu
                        ClientMenuDialog clientMenu = new ClientMenuDialog(AuctionHouse);
                        clientMenu.Display();
                        return;
                    }
                }
            }
            WriteLine("\tYou have entered an invalid email address or password");
        }
    }
}
