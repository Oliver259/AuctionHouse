namespace AuctionHouse.Dialogs
{
    /// <summary>
    /// Base class for all dialogs.
    /// </summary>
    public abstract class Dialog
    {
        /// <summary>
        /// Reference to Auction House.
        /// </summary>
        protected AuctionHouse AuctionHouse { get; }

        /// <summary>
        /// Initialise the Dialog.
        /// </summary>
        /// <param name="auctionHouse">Reference to Auction House.</param>
        public Dialog(AuctionHouse auctionHouse)
        {
            AuctionHouse = auctionHouse;
        }

        /// <summary>
        /// Display and execute the logic of the displayable object.
        /// </summary>
        public abstract void Display();
    }
}
