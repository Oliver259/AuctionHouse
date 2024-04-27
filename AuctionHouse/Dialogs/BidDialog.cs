using AuctionHouse.Verifiers;
using static System.Console;

namespace AuctionHouse.Dialogs
{
    /// <summary>
    /// Queries the client if they want to bid, if they do then it prompts them with the bid amount. The client is then taken to the delivery options dialog.
    /// </summary>
    public class BidDialog : Dialog
    {
        private List<Product> biddableProducts;

        /// <summary>
        /// Initialise Dialog.
        /// </summary>
        /// <param name="auctionHouse">Auction House where products are sold.</param>
        /// <param name="biddableProducts">Products that can be bid on</param>
        public BidDialog(AuctionHouse auctionHouse, List<Product> biddableProducts) : base(auctionHouse)
        {
            this.biddableProducts = biddableProducts;
        }

        public override void Display()
        {
            string bidConfirmation = Util.ReadString("\n\nWould you like to place a bid on any of these items (yes or no)?", new BidConfirmationVerifier());

            if (bidConfirmation == "no")
            {
                return;
            }

            int numProducts = biddableProducts.Count();
            uint rowNumber = Util.ReadUint($"\nPlease enter a non-negative integer between 1 and {numProducts}:", new NumberRangeVerifier(1, numProducts, $"\tItem # must be between an integer between 1 and {numProducts}"));

            Product chosenProduct = biddableProducts[(int)rowNumber - 1];

            Bid highestBid = AuctionHouse.GetHighestBid(chosenProduct);
            decimal currentBidAmount = 0;
            if (highestBid != null)
            {
                currentBidAmount = highestBid.bidAmount;
            }
            WriteLine($"\nBidding for {chosenProduct.productName} (regular price {chosenProduct.productPrice:C}), current highest bid {currentBidAmount:C}");

            while (true)
            {
                string bidAmountInput = Util.ReadString("\n\nHow much do you bid?", new CurrencyVerifier());

                // Convert productPrice to decimal
                decimal bidAmount;
                decimal.TryParse(bidAmountInput.Substring(1), out bidAmount);


                if (bidAmount < currentBidAmount)
                {
                    WriteLine($"\tBid amount must be greater than ${currentBidAmount}");
                }

                else if (bidAmount == 0)
                {
                    WriteLine($"\tBid amount must be greater than $0.00");
                }

                else
                {
                    Client bidder = AuctionHouse.loggedInClient;

                    Bid bid = AddBid(bidder, bidAmount, chosenProduct);

                    WriteLine($"\nYour bid of ${bidAmount} for {chosenProduct.productName} is placed.");

                    DeliveryOptionsDialog deliveryOptionsDialog = new DeliveryOptionsDialog(AuctionHouse, bid);
                    deliveryOptionsDialog.Display();
                    return;
                }

            }
        }

        /// <summary>
        /// Adds a new bid to a chosen product then saves bids, clients and products.
        /// </summary>
        /// <param name="bidder">The client who bidded on a product.</param>
        /// <param name="bidAmount">The amount the bidder bidded on a product.</param>
        /// <param name="chosenProduct">The product which was bidded on.</param>
        /// <returns></returns>
        private Bid AddBid(Client bidder, decimal bidAmount, Product chosenProduct)
        {
            Bid bid = new Bid(bidder, bidAmount, chosenProduct);

            AuctionHouse.bids.Add(bid);

            AuctionHouse.SaveBids();
            AuctionHouse.SaveClients();
            AuctionHouse.SaveProducts();
            return bid;
        }
    }
}
