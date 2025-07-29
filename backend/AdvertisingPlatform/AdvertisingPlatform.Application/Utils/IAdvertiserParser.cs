namespace AdvertisingPlatform.Application.Utils
{
    public interface IAdvertiserParser
    {
        /// <summary>
        /// Parses a stream containing advertiser-location data.
        /// Each entry associates an advertiser with a location path, split into segments.
        /// </summary>
        /// <param name="inputStream">The input stream containing raw data to parse.</param>
        /// <returns>
        /// A collection of tuples, each containing an advertiser name and the corresponding location path segments.
        /// Segments are in nesting order.
        /// </returns>
        Task<IEnumerable<(string Advertiser, string[] LocationParts)>> ParseAsync(Stream inputStream);

        /// <summary>
        /// Parses a raw location path into its individual segments.
        /// Example: "/ru/svrd/revda" → ["ru", "svrd", "revda"].
        /// Segments are in nesting order.
        /// </summary>
        /// <param name="locationPath">The raw location path string.</param>
        /// <returns>An array of parsed location segments. Segments are in nesting order.</returns>
        Task<string[]> ParseLocationAsync(string locationPath);
    }
}
