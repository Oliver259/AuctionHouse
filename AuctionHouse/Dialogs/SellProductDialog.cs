using AuctionHouse.Verifiers;
using static System.Console;

namespace AuctionHouse.Dialogs
{
    /// <summary>
    /// Asks the client if they want to sell a product to the highest bidder. If they do it removes the product from the list of available products to bid on and adds the product to the highest bidder's list of purchased products.
    /// </summary>
    public class SellProductDialog : Dialog
    {
        /// <summary>
        /// A list of products that are able to be bidded on.
        /// </summary>
        private List<Product> biddableProducts;


        /// <summary>
        /// Initialise Dialog.
        /// </summary>
        /// <param name="auctionHouse">Auction House where products are sold.</param>
        /// <param name="biddableProducts">List of products that are able to be bidded on.</param>
        public SellProductDialog(AuctionHouse auctionHouse, List<Product> biddableProducts) : base(auctionHouse)
        {
            this.biddableProducts = biddableProducts;
        }

        public override void Display()
        {
            // Prompt the client for their product name, verify if the product name input is a non-blank text string and then store it
            string sellConfirmation = Util.ReadString("\nWould you like to sell something (yes or no)?", new BidConfirmationVerifier());

            if (sellConfirmation == "no")
            {
                return;
            }

            int numProducts = biddableProducts.Count();

            // Prompt the client for the row number of the produc they want to purchase and store it
            uint rowNumber = Util.ReadUint($"\nPlease enter an integer between 1 and {numProducts}:", new NumberRangeVerifier(1, numProducts, $"Item # must be between an integer 1 and {numProducts}"));

            Product chosenProduct = biddableProducts[(int)rowNumber - 1];

            Bid highestBid = AuctionHouse.GetHighestBid(chosenProduct);
            decimal currentBidAmount = 0;
            if (highestBid != null)
            {
                currentBidAmount = highestBid.bidAmount;
            }

            SellProduct(chosenProduct, highestBid);

            WriteLine($"\nYou have sold {chosenProduct.productName} to {highestBid.bidder.Name} for {currentBidAmount:C}.");
        }

        /// <summary>
        /// Sells a product to the highest bidder.
        /// </summary>
        /// <param name="chosenProduct"></param>
        /// <param name="highestBid"></param>
        private void SellProduct(Product chosenProduct, Bid highestBid)
        {
            // Remove the chosen product from the logged in client's product list
            AuctionHouse.loggedInClient.clientProducts.Remove(chosenProduct);

            // Add the chosen product the the highest bidder's purchasedProducts list
            highestBid.bidder.purchasedProducts.Add(chosenProduct);

            AuctionHouse.products.Remove(chosenProduct);

            AuctionHouse.SaveBids();
            AuctionHouse.SaveClients();
            AuctionHouse.SaveProducts();
        }
    }
}

