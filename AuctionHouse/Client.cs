namespace AuctionHouse
{
    /// <summary>
    /// Stores information about client.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Client's name (read-only).
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Client's email address (read-only).
        /// </summary>
        public string emailAddress { get; }

        /// <summary>
        /// Client's password (read-only).
        /// </summary>
        public string password { get; }

        /// <summary>
        /// Client's home address (read-write).
        /// </summary>
        public Address? homeAddress { get; set; }

        /// <summary>
        /// If client has signed in before (read-write).
        /// </summary>
        public bool hasSignedIn { get; set; }

        /// <summary>
        /// List of client's products.
        /// </summary>
        public List<Product> clientProducts { get; set; }

        /// <summary>
        /// List of a client's purchased products.
        /// </summary>
        public List<Product> purchasedProducts { get; set; }

        /// <summary>
        /// Creates a new client.
        /// </summary>
        /// <param name="Name">Client's name.</param>
        /// <param name="emailAddress">Client's email address.</param>
        /// <param name="password">Client's password.</param>
        public Client(string Name, string emailAddress, string password)
        {
            this.Name = Name;
            this.emailAddress = emailAddress;
            this.password = password;
            clientProducts = new List<Product>();
            purchasedProducts = new List<Product>();
        }
    }
}
