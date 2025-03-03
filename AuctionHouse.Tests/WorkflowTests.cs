namespace AuctionHouse.Tests
{
    public class WorkflowTests
    {
        [Fact]
        public void BidAndSellWorkflow_CompletesSuccessfully()
        {
            // Arrange
            var auctionHouse = new AuctionHouse();
            
            // Create seller and buyer
            var seller = new Client("Seller", "seller@example.com", "Password1!");
            var buyer = new Client("Buyer", "buyer@example.com", "Password2!");
            
            // Add clients to auction house
            auctionHouse.clients.Add(seller);
            auctionHouse.clients.Add(buyer);
            
            // Seller creates a product
            var product = new Product(1, "Test Item", "A great test item", 100.00m, "seller@example.com");
            seller.clientProducts.Add(product);
            auctionHouse.products.Add(product);
            
            // Buyer places a bid
            var bid = new Bid(buyer, 150.00m, product);
            auctionHouse.bids.Add(bid);
            
            // Simulate the sell process
            seller.clientProducts.Remove(product);
            buyer.purchasedProducts.Add(product);
            auctionHouse.products.Remove(product);
            
            // Assert
            Assert.DoesNotContain(product, seller.clientProducts);
            Assert.Contains(product, buyer.purchasedProducts);
            Assert.DoesNotContain(product, auctionHouse.products);
            
            // Verify the highest bid logic
            var highestBid = auctionHouse.GetHighestBid(product);
            Assert.Equal(bid, highestBid);
        }
        
        [Fact]
        public void AddressHandling_WorksCorrectly()
        {
            // Arrange
            var address = new Address(5, 123, "Main", "St", "Brisbane", 4000, "QLD");
            var client = new Client("Test User", "test@example.com", "Password1!")
            {
                // Act - Simulate first-time login
                homeAddress = address,
                hasSignedIn = true
            };

            // Assert
            Assert.Equal(address, client.homeAddress);
            Assert.True(client.hasSignedIn);
            Assert.Equal("5/123 Main St, Brisbane QLD 4000", client.homeAddress.ToString());
        }
    }
}