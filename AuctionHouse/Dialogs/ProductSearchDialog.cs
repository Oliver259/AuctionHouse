using static System.Console;

namespace AuctionHouse.Dialogs
{
    /// <summary>
    /// Asks the user to search for products which will then display any matching products for the client to bid on through the bid dialog..
    /// </summary>
    public class ProductSearchDialog : Dialog
    {
        /// <summary>
        /// Initialise Dialog.
        /// </summary>
        /// <param name="auctionHouse">Auction House where products are sold.</param>
        public ProductSearchDialog(AuctionHouse auctionHouse) : base(auctionHouse)
        {
        }

        public override void Display()
        {
            WriteLine("");

            AuctionHouse.AddHeading($"Product Search for {AuctionHouse.loggedInClient.Name}({AuctionHouse.loggedInClient.emailAddress})");

            List<Product> matchingProducts = AuctionHouse.SearchProducts();


            if (matchingProducts.Count != 0)
            {
                BidDialog bidDialog = new BidDialog(AuctionHouse, matchingProducts);
                bidDialog.Display();
            }
            else
            {
                return;
            }

        }
    }
}
