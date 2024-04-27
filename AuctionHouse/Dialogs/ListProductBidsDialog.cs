using static System.Console;

namespace AuctionHouse.Dialogs
{
    /// <summary>
    /// Lists any bids for the logged in clients products they listed.
    /// </summary>
    public class ListProductBidsDialog : Dialog
    {
        /// <summary>
        /// Initialise Dialog.
        /// </summary>
        /// <param name="auctionHouse">Auction House where products are sold.</param>
        public ListProductBidsDialog(AuctionHouse auctionHouse) : base(auctionHouse)
        {
        }

        public override void Display()
        {
            int rowCounter = 0;

            WriteLine();
            WriteLine();
            AuctionHouse.AddHeading($"List Product Bids for {AuctionHouse.loggedInClient.Name}({AuctionHouse.loggedInClient.emailAddress})");

            List<Product> biddableProducts = AuctionHouse.DisplayProductBids();

            int numProducts = biddableProducts.Count();
            if (numProducts == 0)
            {
                return;
            }

            SellProductDialog sellProductDialog = new SellProductDialog(AuctionHouse, biddableProducts);
            sellProductDialog.Display();

        }
    }
}
