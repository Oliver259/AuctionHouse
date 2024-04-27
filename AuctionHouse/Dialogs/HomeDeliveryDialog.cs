using static System.Console;

namespace AuctionHouse.Dialogs
{
    /// <summary>
    /// Asks the user to provide a valid delivery address. This delivery adddress will be sent the product if the client's bid was successful.
    /// </summary>
    public class HomeDeliveryDialog : Dialog
    {
        private Bid bid;

        /// <summary>
        /// Initialise Dialog.
        /// </summary>
        /// <param name="auctionHouse">Auction House where products are sold.</param>
        /// <param name="bid">Bid for a product.</param>
        public HomeDeliveryDialog(AuctionHouse auctionHouse, Bid bid) : base(auctionHouse)
        {
            this.bid = bid;
        }

        public override void Display()
        {
            WriteLine("\nPlease provide your delivery address.");

            Address deliveryAddress = Util.ReadAddress();

            WriteLine($"\nThank you for your bid. If successful, the item will be provided via delivery to {deliveryAddress}");

            AddDeliveryAddress(deliveryAddress);

        }

        /// <summary>
        /// Adds a delivery address to a bid.
        /// </summary>
        /// <param name="deliveryAddress">Delivery address for a bid.</param>
        private void AddDeliveryAddress(Address deliveryAddress)
        {
            bid.deliveryAddress = deliveryAddress;
            AuctionHouse.SaveBids();
        }
    }
}
