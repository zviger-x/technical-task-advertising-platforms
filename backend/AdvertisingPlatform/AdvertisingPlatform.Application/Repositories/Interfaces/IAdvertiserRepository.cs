namespace AdvertisingPlatform.Application.Repositories.Interfaces
{
    public interface IAdvertiserRepository
    {
        /// <summary>
        /// Replaces all advertisers and their associated location paths.
        /// Clears the entire storage and loads the new dataset.
        /// </summary>
        /// <param name="advertiserLocations">
        /// A collection of tuples where each tuple contains an advertiser name and their location path segments.
        /// Segments go in nesting order.
        /// </param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        Task SetAllAsync(IEnumerable<(string Advertiser, string[] LocationParts)> advertiserLocations, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all advertisers relevant to the given location path.
        /// Aggregates advertisers from the specified location and all parent locations.
        /// </summary>
        /// <param name="locationParts">An array of location segments (e.g., ["ru", "svrd", "revda"], which go in nesting order).</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>An array of matching advertiser names.</returns>
        Task<string[]> GetAdvertisersAsync(string[] locationParts, CancellationToken cancellationToken = default);
    }
}
