using static System.Console;

namespace AuctionHouse.Dialogs
{
    /// <summary>
    /// Displays a list of the logged in client's currently advertised products.
    /// </summary>
    public class ProductListDialog : Dialog
    {
        /// <summary>
        /// Initialise Dialog.
        /// </summary>
        /// <param name="auctionHouse">Auction House where products are sold.</param>
        public ProductListDialog(AuctionHouse auctionHouse) : base(auctionHouse)
        {
        }

        public override void Display()
        {
            int rowCounter = 0;
            WriteLine("");

            AuctionHouse.AddHeading($"Product List for {AuctionHouse.loggedInClient.Name}({AuctionHouse.loggedInClient.emailAddress})");

            WriteLine();

            AuctionHouse.DisplayLoggedInClientsProducts();

        }
    }
}
