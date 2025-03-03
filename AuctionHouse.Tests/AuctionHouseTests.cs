namespace AuctionHouse.Tests
{
    public class AuctionHouseTests
    {
        [Fact]
        public void GetHighestBid_ReturnsCorrectBid()
        {
            // Arrange
            var auctionHouse = new AuctionHouse();
            var client1 = new Client("User1", "user1@example.com", "Password1!");
            var client2 = new Client("User2", "user2@example.com", "Password2!");
            var product = new Product(1, "Test Product", "Description", 100.00m, "seller@example.com");
            
            var bid1 = new Bid(client1, 120.00m, product);
            var bid2 = new Bid(client2, 150.00m, product); // Higher bid
            
            auctionHouse.bids.Add(bid1);
            auctionHouse.bids.Add(bid2);
            
            // Act
            var highestBid = auctionHouse.GetHighestBid(product);
            
            // Assert
            Assert.Equal(bid2, highestBid);
        }
        
        [Fact]
        public void GetHighestBid_WithNoBids_ReturnsNull()
        {
            // Arrange
            var auctionHouse = new AuctionHouse();
            var product = new Product(1, "Test Product", "Description", 100.00m, "seller@example.com");
            
            // Act
            var highestBid = auctionHouse.GetHighestBid(product);
            
            // Assert
            Assert.Null(highestBid);
        }
        
        [Fact]
        public void DisplayProductBids_WithNoBids_ShowsNoProductsMessage()
        {
            // Arrange
            using var console = new ConsoleRedirector("");
            var auctionHouse = new AuctionHouse();
            var client = new Client("Test User", "test@example.com", "Password1!");
            auctionHouse.loggedInClient = client;
            
            // Act
            var products = auctionHouse.DisplayProductBids();
            
            // Assert
            Assert.Empty(products);
            Assert.Contains("No bids were found", console.outputWriter.ToString());
        }
    }
}