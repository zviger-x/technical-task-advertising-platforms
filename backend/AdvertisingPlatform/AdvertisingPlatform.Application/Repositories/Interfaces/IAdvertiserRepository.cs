namespace AdvertisingPlatform.Application.Repositories.Interfaces
{
    public interface IAdvertiserRepository
    {
        /// <summary>
        /// Adds an advertiser with a set of location paths.
        /// Each location is represented as an array of location segments (e.g., ["ru", "svrd", "revda"], which go in nesting order).
        /// </summary>
        /// <param name="advertiser">The name of the advertiser to add.</param>
        /// <param name="locationParts">A collection of location paths represented as arrays of segments.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        Task AddAsync(string advertiser, IEnumerable<string[]> locationParts, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all advertisers relevant to the given location path.
        /// Aggregates advertisers from the specified location and all parent locations.
        /// </summary>
        /// <param name="locationParts">An array of location segments (e.g., ["ru", "svrd", "revda"], which go in nesting order).</param>
        /// <param name="cancellationToken">Optional token to cancel the operation.</param>
        /// <returns>An array of matching advertiser names.</returns>
        Task<string[]> GetAdvertisersAsync(string[] locationParts, CancellationToken cancellationToken = default);
    }
}
