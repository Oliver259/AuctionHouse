using static System.Console;

namespace AuctionHouse.Dialogs
{
    /// <summary>
    /// Displays any items that the logged in client has successfully purchased.
    /// </summary>
    public class PurchasedItemsDialog : Dialog
    {
        /// <summary>
        /// Initialise Dialog.
        /// </summary>
        /// <param name="auctionHouse">Auction House where products are sold.</param>
        public PurchasedItemsDialog(AuctionHouse auctionHouse) : base(auctionHouse)
        {
        }

        public override void Display()
        {
            int rowCounter = 0;
            WriteLine("");
            AuctionHouse.AddHeading($"Purchased Items for {AuctionHouse.loggedInClient.Name}({AuctionHouse.loggedInClient.emailAddress})");
            AuctionHouse.DisplayPurchasedItems();

        }
    }
}
