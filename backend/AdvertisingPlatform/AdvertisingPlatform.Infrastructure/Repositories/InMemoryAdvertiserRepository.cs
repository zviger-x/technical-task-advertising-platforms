using AdvertisingPlatform.Application.Repositories.Interfaces;
using AdvertisingPlatform.Infrastructure.Contexts.InMemory;

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

            _context.SetAdvertisers(advertiserLocations);

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
