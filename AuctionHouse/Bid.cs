namespace AuctionHouse
{
    /// <summary>
    /// Stores information about bids.
    /// </summary>
    public class Bid
    {
        /// <summary>
        /// Person who bidded on a product (read-only).
        /// </summary>
        public Client bidder { get; }

        /// <summary>
        /// Amount a bidder bidded on a product (read-only).
        /// </summary>
        public decimal bidAmount { get; }

        /// <summary>
        /// The product which was bidded on (read-only).
        /// </summary>
        public Product biddedProduct { get; }

        /// <summary>
        /// The date and time a bid was created (read-only).
        /// </summary>
        public DateTime creationDateTime { get; }

        /// <summary>
        /// The delivery address for a bidded product (read-write).
        /// </summary>
        public Address deliveryAddress { get; set; }

        /// <summary>
        /// A delivery's start date and time (read-write).
        /// </summary>
        public DateTime deliveryStartDateTime { get; set; }

        /// <summary>
        /// A delivery's end date and time (read-write).
        /// </summary>
        public DateTime deliveryEndDateTime { get; set; }

        /// <summary>
        /// Creates a new bid.
        /// </summary>
        /// <param name="bidder">Client who bidded on a product.</param>
        /// <param name="bidAmount">The amount a client bidded on a product.</param>
        /// <param name="biddedProduct">The product which a bidder bidded on.</param>
        public Bid(Client bidder, decimal bidAmount, Product biddedProduct)
        {
            this.bidder = bidder;
            this.bidAmount = bidAmount;
            this.biddedProduct = biddedProduct;
            this.creationDateTime = DateTime.Now;
        }
    }
}
