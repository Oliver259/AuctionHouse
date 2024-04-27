namespace AuctionHouse
{
    /// <summary>
    /// Stores information about product's.
    /// </summary>
    public class Product : IComparable<Product>
    {
        /// <summary>
        /// Product's name (read-only).
        /// </summary>
        public string productName { get; }

        /// <summary>
        /// Product's description (read-only).
        /// </summary>
        public string productDescription { get; }

        /// <summary>
        /// Product's price (read-only).
        /// </summary>
        public decimal productPrice { get; }

        /// <summary>
        /// Product's ID (read-only).
        /// </summary>
        public int productID { get; }

        /// <summary>
        /// The email of a product's owner (read-only).
        /// </summary>
        public string ownerEmail { get; }

        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <param name="productID">Product's ID.</param>
        /// <param name="productName">Product's name.</param>
        /// <param name="productDescription">Product's description.</param>
        /// <param name="productPrice">Product's price.</param>
        /// <param name="ownerEmail">The email of a product's owner.</param>
        public Product(int productID, string productName, string productDescription, decimal productPrice, string ownerEmail)
        {
            this.productID = productID;
            this.productName = productName;
            this.productDescription = productDescription;
            this.productPrice = productPrice;
            this.ownerEmail = ownerEmail;
        }

        /// <summary>
        /// Compares two products.
        /// </summary>
        /// <param name="other">Other product it is comparing against.</param>
        /// <returns>Returns 1 if there is no product to compare to.</returns>
        public int CompareTo(Product? other)
        {
            if (other == null)
            {
                return 1;
            }

            int productNameComparison = string.Compare(productName, other.productName);
            if (productNameComparison != 0) { return productNameComparison; }

            int productDescriptionComparison = string.Compare(productDescription, other.productDescription);
            if (productDescriptionComparison != 0) { return productDescriptionComparison; }

            int productPriceComparison = decimal.Compare(productPrice, other.productPrice);
            return productPriceComparison;


        }

        /// <summary>
        /// Gets a string representing a product.
        /// </summary>
        /// <returns>A string representing a product.</returns>
        public override string ToString()
        {
            return $"{productName}\t{productDescription}\t{productPrice}";
        }

        /// <summary>
        /// Check if two products equal each other.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Returns true if two products equal each other.</returns>
        public override bool Equals(object? obj)
        {
            var other = obj as Product;
            return productID == other.productID;
        }
    }
}
