namespace AuctionHouse.Tests
{
    public class ModelTests
    {
        [Fact]
        public void Client_ConstructorSetsPropertiesCorrectly()
        {
            // Arrange & Act
            var client = new Client("Test User", "test@example.com", "Password123!");
            
            // Assert
            Assert.Equal("Test User", client.Name);
            Assert.Equal("test@example.com", client.emailAddress);
            Assert.Equal("Password123!", client.password);
            Assert.False(client.hasSignedIn);
            Assert.NotNull(client.clientProducts);
            Assert.NotNull(client.purchasedProducts);
        }
        
        [Fact]
        public void Product_ConstructorSetsPropertiesCorrectly()
        {
            // Arrange & Act
            var product = new Product(1, "Test Product", "Test Description", 100.00m, "seller@example.com");
            
            // Assert
            Assert.Equal(1, product.productID);
            Assert.Equal("Test Product", product.productName);
            Assert.Equal("Test Description", product.productDescription);
            Assert.Equal(100.00m, product.productPrice);
            Assert.Equal("seller@example.com", product.ownerEmail);
        }
        
        [Fact]
        public void Product_CompareToSortsCorrectly()
        {
            // Arrange
            var product1 = new Product(1, "A", "Description", 100.00m, "seller@example.com");
            var product2 = new Product(2, "B", "Description", 100.00m, "seller@example.com");
            var product3 = new Product(3, "A", "A Description", 100.00m, "seller@example.com");
            var product4 = new Product(4, "A", "Description", 50.00m, "seller@example.com");
            
            // Act & Assert
            Assert.True(product1.CompareTo(product2) < 0); // A comes before B
            Assert.True(product3.CompareTo(product1) < 0); // Same name, A Description comes before Description
            Assert.True(product4.CompareTo(product1) < 0); // Same name and description, 50 comes before 100
        }
        
        [Fact]
        public void Address_ToStringFormatsCorrectly_WithUnitNumber()
        {
            // Arrange
            var address = new Address(5, 123, "Main", "St", "Brisbane", 4000, "QLD");
            
            // Act
            string result = address.ToString();
            
            // Assert
            Assert.Equal("5/123 Main St, Brisbane QLD 4000", result);
        }
        
        [Fact]
        public void Address_ToStringFormatsCorrectly_WithoutUnitNumber()
        {
            // Arrange
            var address = new Address(0, 123, "Main", "St", "Brisbane", 4000, "QLD");
            
            // Act
            string result = address.ToString();
            
            // Assert
            Assert.Equal("123 Main St, Brisbane QLD 4000", result);
        }
        
        [Fact]
        public void Bid_ConstructorSetsPropertiesCorrectly()
        {
            // Arrange
            var client = new Client("Test User", "test@example.com", "Password123!");
            var product = new Product(1, "Test Product", "Test Description", 100.00m, "seller@example.com");
            
            // Act
            var bid = new Bid(client, 150.00m, product);
            
            // Assert
            Assert.Equal(client, bid.bidder);
            Assert.Equal(150.00m, bid.bidAmount);
            Assert.Equal(product, bid.biddedProduct);
            Assert.True((DateTime.Now - bid.creationDateTime).TotalSeconds < 5); // Created just now
        }
    }
}