using AdvertisingPlatform.Infrastructure.Contexts.InMemory.Models;

namespace AdvertisingPlatform.Infrastructure.Contexts.InMemory
{
    /// <summary>
    /// Should be used as a single instance or singleton in DI.
    /// </summary>
    public sealed class InMemoryContext
    {
        private readonly LocationTree _locationTree = new();

        public void SetAdvertisers(IEnumerable<(string Advertiser, string[][] Locations)> advertiserLocations)
        {
            _locationTree.SetAdvertisers(advertiserLocations);
        }

        public string[] FindAdvertisers(string[] locationParts)
        {
            return _locationTree.FindAdvertisers(locationParts);
        }
    }
}
