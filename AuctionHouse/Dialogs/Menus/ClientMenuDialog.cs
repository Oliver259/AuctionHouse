using static System.Console;

namespace AuctionHouse.Dialogs.Menus
{
    /// <summary>
    /// Client menu for Auction House system which is only accessable by clients once logged in.
    /// </summary>
    public class ClientMenuDialog : Dialog
    {
        const int ADVERTISE_PRODUCT = 1, VIEW_PRODUCT_LIST = 2, ADVERTISED_PRODUCT_SEARCH = 3, VIEW_BIDS = 4, VIEW_PURCHASES = 5, LOG_OFF = 6;

        /// <summary>
        /// Initialise MainMenuDialog
        /// </summary>
        /// <param name="auctionHouse">Auction House where products are sold.</param>
        public ClientMenuDialog(AuctionHouse auctionHouse) : base(auctionHouse)
        {
        }

        public override void Display()
        {
            while (true)
            {
                // Display Main Menu
                AuctionHouse.AddHeading("\nClient Menu");
                WriteLine($"({ADVERTISE_PRODUCT}) Advertise Product");
                WriteLine($"({VIEW_PRODUCT_LIST}) View My Product List");
                WriteLine($"({ADVERTISED_PRODUCT_SEARCH}) Search For Advertised Products");
                WriteLine($"({VIEW_BIDS}) View Bids On My Products");
                WriteLine($"({VIEW_PURCHASES}) View My Purchased Items");
                WriteLine($"({LOG_OFF}) Log off");
                WriteLine("");

                // Read integer
                uint option = Util.ReadUint("Please select an option between 1 and 6");

                if (option >= 1 && option < LOG_OFF)
                {
                    Process(option);
                }
                else if (option == LOG_OFF)
                {
                    break;
                }
                // Error message
                else
                {
                    WriteLine($"Option must be greater than or equal to 1 and less than or equal to {LOG_OFF}.");
                }
            }
        }

        /// <summary>
        /// Processes user input.
        /// </summary>
        /// <param name="option">The option inputted by the user.</param>
        private void Process(uint option)
        {
            if (option == ADVERTISE_PRODUCT)
            {
                ProductAdvertisementDialog productAdvertisement = new ProductAdvertisementDialog(AuctionHouse);

                //Registration dlg = new Registration("Register New Client", Bank);
                productAdvertisement.Display();
            }

            else if (option == VIEW_PRODUCT_LIST)
            {
                ProductListDialog productList = new ProductListDialog(AuctionHouse);
                productList.Display();
            }
            else if (option == ADVERTISED_PRODUCT_SEARCH)
            {
                ProductSearchDialog productSearch = new ProductSearchDialog(AuctionHouse);
                productSearch.Display();
            }

            else if (option == VIEW_BIDS)
            {
                ListProductBidsDialog listProductBids = new ListProductBidsDialog(AuctionHouse);
                listProductBids.Display();
            }

            else if (option == VIEW_PURCHASES)
            {
                PurchasedItemsDialog purchasedItemsDialog = new PurchasedItemsDialog(AuctionHouse);
                purchasedItemsDialog.Display();
            }
        }
    }
}
