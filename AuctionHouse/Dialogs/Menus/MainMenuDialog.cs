using static System.Console;

namespace AuctionHouse.Dialogs.Menus
{
    /// <summary>
    /// Main menu for Auction House system.
    /// </summary>
    public class MainMenuDialog : Dialog
    {
        const int REGISTER = 1, SIGNIN = 2, EXIT = 3;

        /// <summary>
        /// Initialise MainMenuDialog
        /// </summary>
        /// <param name="auctionHouse">Auction House where products are sold.</param>
        public MainMenuDialog(AuctionHouse auctionHouse) : base(auctionHouse)
        {
        }

        public override void Display()
        {
            while (true)
            {
                // Display Main Menu
                AuctionHouse.AddHeading("\nMain Menu");
                WriteLine($"({REGISTER}) Register");
                WriteLine($"({SIGNIN}) Sign In");
                WriteLine($"({EXIT}) Exit");
                WriteLine("");


                // Read integer
                uint option = Util.ReadUint("Please select an option between 1 and 3");

                if (option >= 1 && option < EXIT)
                {
                    Process(option);
                }
                else if (option == EXIT)
                {
                    // Display farewell message to client then close the program
                    WriteLine("+--------------------------------------------------+");
                    WriteLine("| Good bye, thank you for using the Auction House! |");
                    WriteLine("+--------------------------------------------------+");

                    Environment.Exit(-1);
                }

                else
                {
                    WriteLine($"Option must be greater than or equal to 1 and less than or equal to {EXIT}.");
                }
            }
        }

        /// <summary>
        /// Processes user input.
        /// </summary>
        /// <param name="option">The option inputted by the user.</param>
        private void Process(uint option)
        {
            if (option == REGISTER)
            {
                RegistrationDialog registration = new RegistrationDialog(AuctionHouse);
                registration.Display();
            }

            else if (option == SIGNIN)
            {
                SiginDialog signin = new SiginDialog(AuctionHouse);
                signin.Display();
            }
        }
    }
}
