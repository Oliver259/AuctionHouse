using AuctionHouse.Verifiers;
using static System.Console;

namespace AuctionHouse.Dialogs
{
    /// <summary>
    /// Queries the client for a valid pickup window start and end time so the client will know when their potential delivery will be delivered to them.
    /// </summary>
    public class ClickandCollectDialog : Dialog
    {
        private Bid bid;

        /// <summary>
        /// Initialsie Dialog.
        /// </summary>
        /// <param name="auctionHouse">Auction House where products are sold.</param>
        /// <param name="bid">Bid for a product.</param>
        public ClickandCollectDialog(AuctionHouse auctionHouse, Bid bid) : base(auctionHouse)
        {
            this.bid = bid;
        }

        public override void Display()
        {
            PickupWindowVerifier pickupWindowVerifierStart = new PickupWindowVerifier(bid.creationDateTime, "\tDelivery window start must be at least one hour in the future.");
            DateTime pickupWindowStart = Util.ReadDateTime("\nDelivery window start (dd/mm/yyyy hh:mm)", pickupWindowVerifierStart);

            PickupWindowVerifier pickupWindowVerifierEnd = new PickupWindowVerifier(pickupWindowStart, "\tDelivery window end must be at least one hour later than the start.");
            DateTime pickupWindowEnd = Util.ReadDateTime("\nDelivery window end (dd/mm/yyyy hh:mm)", pickupWindowVerifierEnd);

            WriteLine($"\nThank you for your bid. If succesful, the item will be provided via collection between {pickupWindowStart:t} on {pickupWindowStart:d} and {pickupWindowEnd:t} on {pickupWindowEnd:d} ");

            bid.deliveryStartDateTime = pickupWindowStart;
            bid.deliveryEndDateTime = pickupWindowEnd;

            AuctionHouse.SaveBids();
        }
    }
}
