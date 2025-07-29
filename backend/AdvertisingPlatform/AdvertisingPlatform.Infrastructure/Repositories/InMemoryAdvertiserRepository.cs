using AdvertisingPlatform.Application.Repositories.Interfaces;
using AdvertisingPlatform.Infrastructure.Contexts.InMemory;
using AdvertisingPlatform.Infrastructure.Contexts.InMemory.Models;

namespace AdvertisingPlatform.Infrastructure.Repositories
{
    public sealed class InMemoryAdvertiserRepository : IAdvertiserRepository
    {
        private readonly InMemoryContext _context;

        public InMemoryAdvertiserRepository(InMemoryContext context)
        {
            _context = context;
        }

        public Task SetAllAsync(IEnumerable<(string Advertiser, string[][] Locations)> advertiserLocations, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var advLocs = advertiserLocations
                .Select(x => new AdvertiserLocations { Advertiser = x.Advertiser, Locations = x.Locations })
                .ToList();

            _context.SetAdvertisers(advLocs);

            return Task.CompletedTask;
        }

        public Task<string[]> GetAdvertisersAsync(string[] locationParts, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var advertisers = _context.FindAdvertisers(locationParts);

            return Task.FromResult(advertisers);
        }
    }
}
