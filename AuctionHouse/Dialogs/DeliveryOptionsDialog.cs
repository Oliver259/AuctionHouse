using static System.Console;

namespace AuctionHouse.Dialogs
{
    /// <summary>
    /// Asks the client which delivery option they want if their bid is successful. It then takes client to the respective dialog.
    /// </summary>
    public class DeliveryOptionsDialog : Dialog
    {
        private const int CLICK_AND_COLLECT = 1, HOME_DELIVERY = 2;
        private Bid bid;

        /// <summary>
        /// Initialise Dialog.
        /// </summary>
        /// <param name="auctionHouse">Auction House where products are sold.</param>
        /// <param name="bid">Bid for a product.</param>
        public DeliveryOptionsDialog(AuctionHouse auctionHouse, Bid bid) : base(auctionHouse)
        {
            this.bid = bid;
        }

        public override void Display()
        {
            // Display Main Menu
            WriteLine("");
            WriteLine("Delivery Instructions");
            WriteLine("---------------------");
            WriteLine($"({CLICK_AND_COLLECT}) Click and collect");
            WriteLine($"({HOME_DELIVERY}) Home Delivery");
            WriteLine("");


            // Read integer
            uint option = Util.ReadUint("Please select an option between 1 and 2");

            if (option == CLICK_AND_COLLECT)
            {
                ClickandCollectDialog clickandCollectDialog = new ClickandCollectDialog(AuctionHouse, bid);
                clickandCollectDialog.Display();
            }
            else if (option == HOME_DELIVERY)
            {
                HomeDeliveryDialog homeDeliveryDialog = new HomeDeliveryDialog(AuctionHouse, bid);
                homeDeliveryDialog.Display();
            }
            // Error message
            else
            {
                WriteLine($"Option must be greater than or equal to 1 and less than or equal to 2.");
            }
        }
    }
}
