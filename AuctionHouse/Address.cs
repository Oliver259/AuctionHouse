namespace AuctionHouse
{
    /// <summary>
    /// Stores information about client's addresses.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Client's unit number (read-only).
        /// </summary>
        public uint unitNumber { get; }

        /// <summary>
        /// Client's street number (read-only).
        /// </summary>
        public uint streetNumber { get; }

        /// <summary>
        /// Client's street name (read-only).
        /// </summary>
        public string streetName { get; }

        /// <summary>
        /// Client's street suffix (read-only).
        /// </summary>
        public string streetSuffix { get; }

        /// <summary>
        /// Client's city (read-only).
        /// </summary>
        public string city { get; }

        /// <summary>
        /// Client's postcode (read-only).
        /// </summary>
        public uint postcode { get; }

        /// <summary>
        /// Client's state (read-only).
        /// </summary>
        public string state { get; }

        /// <summary>
        /// Create a new address.
        /// </summary>
        /// <param name="unitNumber">Client's unit number.</param>
        /// <param name="streetNumber">Client's street number.</param>
        /// <param name="streetName">Client's street name.</param>
        /// <param name="streetSuffix">Client's street suffix.</param>
        /// <param name="city">Client's city.</param>
        /// <param name="postcode">Client's postcode.</param>
        /// <param name="state">Client's state.</param>
        public Address(uint unitNumber, uint streetNumber, string streetName, string streetSuffix, string city, uint postcode, string state)
        {
            this.unitNumber = unitNumber;
            this.streetNumber = streetNumber;
            this.streetName = streetName;
            this.streetSuffix = streetSuffix;
            this.city = city;
            this.postcode = postcode;
            this.state = state;
        }
        /// <summary>
        /// Gets a string representing an address.
        /// </summary>
        /// <returns>A string representing an address.</returns>
        public override string ToString()
        {
            string result = "";
            if (unitNumber != 0)
            {
                result = $"{unitNumber}/";
            }
            return result + $"{streetNumber} {streetName} {streetSuffix}, {city} {state} {postcode}";
        }
    }
}
