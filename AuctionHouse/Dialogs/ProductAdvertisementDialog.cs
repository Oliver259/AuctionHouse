using AuctionHouse.Verifiers;
using static System.Console;

namespace AuctionHouse.Dialogs
{
    public class ProductAdvertisementDialog : Dialog
    {
        /// <summary>
        /// Initialise Dialog.
        /// </summary>
        /// <param name="auctionHouse">Auction House where products are sold.</param>
        public ProductAdvertisementDialog(AuctionHouse auctionHouse) : base(auctionHouse)
        {
        }

        public override void Display()
        {
            WriteLine("");
            AuctionHouse.AddHeading($"\nProduct Advertisement for {AuctionHouse.loggedInClient.Name}({AuctionHouse.loggedInClient.emailAddress})");


            // Prompt the client for their product name, verify if the product name input is a non-blank text string and then store it
            string productName = Util.ReadString("\nProduct name", new NonBlankStringVerifier("product name"));

            // Prompt the client for their product description, verify if the product description input is a non-blank text string and then store it
            string productDescription = Util.ReadString("\nProduct description", new NonBlankStringVerifier("product description"));

            // Prompt the client for their product description, verify if the product description input is a non-blank text string and then store it
            string productPriceInput = Util.ReadString("\nProduct price ($d.cc)", new CurrencyVerifier());

            // Convert productPrice to decimal
            decimal productPrice;
            decimal.TryParse(productPriceInput.Substring(1), out productPrice);

            addAdvertisedProduct(productName, productDescription, productPrice);

            // Display a message to inform the client that the product was successfully added
            WriteLine($"\nSuccessfully added product {productName}, {productDescription}, ${productPrice}.");
        }

        /// <summary>
        /// Adds a product to the product list.
        /// </summary>
        /// <param name="productName">Name of a product.</param>
        /// <param name="productDescription">Description of a product.</param>
        /// <param name="productPrice">Price of a product.</param>
        private void addAdvertisedProduct(string productName, string productDescription, decimal productPrice)
        {
            int productID = AuctionHouse.products.Count() + 1;
            Product product = new Product(productID, productName, productDescription, productPrice, AuctionHouse.loggedInClient.emailAddress);
            // All products
            AuctionHouse.products.Add(product);

            // Logged in client's products
            AuctionHouse.loggedInClient.clientProducts.Add(product);
            AuctionHouse.SaveClients();
            AuctionHouse.SaveProducts();
        }
    }
}
