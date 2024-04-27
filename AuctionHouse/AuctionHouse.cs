using AuctionHouse.Verifiers;
using System.Text.Json;
using static System.Console;

namespace AuctionHouse
{
    public class AuctionHouse
    {
        /// <summary>List of accounts.</summary>
        public List<Client> clients = new List<Client>();

        /// <summary>List of products.</summary>
        public List<Product> products = new List<Product>();

        /// <summary>List of bids.</summary>
        public List<Bid> bids = new List<Bid>();

        /// <summary>Logged in client.</summary>
        public Client loggedInClient { get; set; }

        /// <summary>
        /// Adds a heading.
        /// <paramref name="heading"/>
        /// </summary>
        /// <param name="heading">The heading.</param>
        public void AddHeading(string heading)
        {
            WriteLine($"{heading}");
            for (int i = 0; i < heading.Length - 1; i++)
            {
                Write("-");
            }
            WriteLine();
        }

        /// <summary>
        /// Gets the highest bid for a given product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>The current highest bid for a product.</returns>
        public Bid GetHighestBid(Product product)
        {
            Bid highestBid = null;
            foreach (Bid bid in bids)
            {
                if (product.Equals(bid.biddedProduct))
                {
                    if (highestBid == null || bid.bidAmount > highestBid.bidAmount)
                    {
                        highestBid = bid;
                    }
                }
            }
            return highestBid;
        }

        /// <summary>
        /// Displays the logged in client's products.
        /// </summary>
        public void DisplayLoggedInClientsProducts()
        {
            DisplayProductList(loggedInClient.clientProducts, $"Item #\tProduct name\tDescription\tList price\tBidder name\tBidder email\tBid amt");
        }

        /// <summary>
        /// Displays any bids for a product.
        /// </summary>
        /// <returns>If no products were found to have bids it prints an error message otherwise if productss were found to have bids it displays them.</returns>
        public List<Product> DisplayProductBids()
        {
            List<Product> matchingProducts = new List<Product>();
            foreach (Bid bid in bids)
            {
                if (loggedInClient.clientProducts.Contains(bid.biddedProduct))
                {
                    if (!matchingProducts.Contains(bid.biddedProduct))
                    {
                        matchingProducts.Add(bid.biddedProduct);
                    }
                }
            }

            if (matchingProducts.Count == 0)
            {
                WriteLine("\nNo bids were found.");
                return matchingProducts;
            }
            DisplayProductList(matchingProducts, $"\nItem #\tProduct name\tDescription\tList price\tBidder name\tBidder email\tBid amt");
            return matchingProducts;
        }
        /// <summary>
        /// Displays a logged in client's purchased items.
        /// </summary>
        public void DisplayPurchasedItems()
        {
            if (loggedInClient.purchasedProducts.Count() == 0)
            {
                WriteLine("\nYou have no purchased products at the moment.");
                return;
            }

            loggedInClient.purchasedProducts.Sort();
            int rowCounter = 0;
            int numProductListed = 0;

            WriteLine($"\nItem #\tSeller email\tProduct name\tDescription\tList price\tAmt paid\tDelivery option");

            foreach (Product product in loggedInClient.purchasedProducts)
            {
                Bid bid = GetHighestBid(product);
                numProductListed++;
                string deliveryMsg;
                if (bid.deliveryAddress != null)
                {
                    deliveryMsg = $"Deliver to {bid.deliveryAddress}";
                }
                else
                {
                    deliveryMsg = $"Collect between {bid.deliveryStartDateTime:t} on {bid.deliveryStartDateTime:d} and {bid.deliveryEndDateTime:t} on {bid.deliveryEndDateTime:d}";
                }
                WriteLine($"{++rowCounter}\t{product.ownerEmail}\t{product.productName}\t{product.productDescription}\t{product.productPrice:C}\t{bid.bidAmount:C}\t{deliveryMsg}");
            }
        }

        /// <summary>
        /// Display a list of all  the products.
        /// </summary>
        /// <param name="products">The products to be displayed.</param>
        /// <param name="columns">The columns to be displayed before the products.</param>
        public void DisplayProductList(List<Product> products, string columns)
        {
            products.Sort();
            int rowCounter = 0;

            if (products.Count == 0)
            {
                WriteLine("Error: There are no products are currently advertised");
                return;
            }

            WriteLine(columns);
            foreach (Product product in products)
            {
                Write($"{++rowCounter}\t{product.productName}\t{product.productDescription}\t{product.productPrice:C}\t");


                Bid highestBid = GetHighestBid(product);

                if (highestBid == null)
                {
                    WriteLine("-\t-\t-");
                }
                else
                {
                    WriteLine($"{highestBid.bidder.Name}\t{highestBid.bidder.emailAddress}\t{highestBid.bidAmount:C}");
                }
            }
        }

        /// <summary>
        /// Asks the logged in client to enter a search phrase to search for products.
        /// </summary>
        /// <returns>Returns any products that match the inputted search phrase.</returns>
        public List<Product> SearchProducts()
        {
            string search_phrase = Util.ReadString("\n\nPlease supply a search phrase (ALL to see all products)", new NonBlankStringVerifier("search phrase"));
            List<Product> matchingProducts = new List<Product>();
            foreach (Product product in products)
            {
                if (product.productName.Contains(search_phrase) || product.productDescription.Contains(search_phrase) ||
                    search_phrase == "ALL" && !loggedInClient.clientProducts.Contains(product))
                {
                    matchingProducts.Add(product);
                }
            }

            AddHeading("\nSearch results");

            DisplayProductList(matchingProducts, $"\nItem #\tProduct name\tDescription\tList price\tBidder name\tBidder email\tBid amt");
            return matchingProducts;
        }

        /// <summary>
        /// 
        /// </summary>
        public AuctionHouse()
        {
            Load();
        }

        /// <summary>
        /// Loads the clients, products and bids if any are saved.
        /// </summary>
        private void Load()
        {
            if (File.Exists("Clients.json"))
            {
                string fileName = "Clients.json";
                string jsonString = File.ReadAllText(fileName);
                clients = JsonSerializer.Deserialize<List<Client>>(jsonString)!;

            }
            if (File.Exists("Products.json"))
            {
                string fileName = "Products.json";
                string jsonString = File.ReadAllText(fileName);
                products = JsonSerializer.Deserialize<List<Product>>(jsonString)!;

            }
            if (File.Exists("Bids.json"))
            {
                string fileName = "Bids.json";
                string jsonString = File.ReadAllText(fileName);
                bids = JsonSerializer.Deserialize<List<Bid>>(jsonString)!;

            }

        }
        /// <summary>
        /// Saves the clients to the Clients.json file.
        /// </summary>
        public void SaveClients()
        {
            string fileName = "Clients.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(clients, options);
            File.WriteAllText(fileName, jsonString);
        }
        /// <summary>
        /// Saves the products to the Products.json file.
        /// </summary>
        public void SaveProducts()
        {
            string fileName = "Products.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(products, options);
            File.WriteAllText(fileName, jsonString);
        }
        /// <summary>
        /// Saves the Bids to the Bids.json file.
        /// </summary>
        public void SaveBids()
        {
            string fileName = "Bids.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(bids, options);
            File.WriteAllText(fileName, jsonString);
        }
    }
}


