namespace AuctionHouse.Verifiers
{
    /// <summary>
    /// Base interface used by other verifiers.
    /// </summary>
    public interface IVerifier
    {
        /// <summary>
        /// Verifies input.
        /// </summary>
        /// <param name="ThingToVerify">Input to verify.</param>
        /// <returns>Returns true if input is valid otherwise return false.</returns>
        bool Verify(object ThingToVerify);
    }
}
