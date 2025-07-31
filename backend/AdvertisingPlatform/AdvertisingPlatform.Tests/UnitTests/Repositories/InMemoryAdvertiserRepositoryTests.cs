using AdvertisingPlatform.Application.Repositories.Interfaces;
using AdvertisingPlatform.Infrastructure.Contexts.InMemory;
using AdvertisingPlatform.Infrastructure.Repositories;
using FluentAssertions;
using Xunit;

namespace AdvertisingPlatform.Tests.UnitTests.Repositories
{
    public class InMemoryAdvertiserRepositoryTests
    {
        private readonly InMemoryContext _context;
        private readonly IAdvertiserRepository _repository;

        public InMemoryAdvertiserRepositoryTests()
        {
            _context = new InMemoryContext();
            _repository = new InMemoryAdvertiserRepository(_context);
        }

        [Fact]
        public async Task SetAllAsync_ShouldStoreDataCorrectly()
        {
            // Arrange
            var data = new List<(string Advertiser, string[][] Locations)>
            {
                ("Company 1", new[] { new[] { "ru", "msk" }, new[] { "ru", "spb" } }),
                ("Company 2", new[] { new[] { "ru" } })
            };

            // Act
            await _repository.SetAllAsync(data);

            // Assert
            var advertisersInMsk = await _repository.GetAdvertisersAsync(["ru", "msk"]);
            var advertisersInSpb = await _repository.GetAdvertisersAsync(["ru", "spb"]);
            var advertisersInRu = await _repository.GetAdvertisersAsync(["ru"]);

            advertisersInMsk.Should().BeEquivalentTo("Company 1", "Company 2");
            advertisersInSpb.Should().BeEquivalentTo("Company 1", "Company 2");
            advertisersInRu.Should().BeEquivalentTo("Company 2");
        }

        [Fact]
        public async Task GetAdvertisersAsync_ShouldReturnEmpty_WhenNoMatch()
        {
            // Arrange
            var data = new List<(string Advertiser, string[][] Locations)>
            {
                ("Company", new[] { new[] { "by", "gomel" } })
            };

            await _repository.SetAllAsync(data);

            // Act
            var result = await _repository.GetAdvertisersAsync(["ru", "ekb"]);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAdvertisersAsync_ShouldSupportNestedLocations()
        {
            // Arrange
            var data = new List<(string Advertiser, string[][] Locations)>
            {
                ("LocalCompany", new[] { new[] { "ru", "svrd", "revda" } }),
                ("RegionCompany", new[] { new[] { "ru", "svrd" } }),
                ("NationalCompany", new[] { new[] { "ru" } }),
            };

            await _repository.SetAllAsync(data);

            // Act
            var result = await _repository.GetAdvertisersAsync(["ru", "svrd", "revda"]);

            // Assert
            result.Should().BeEquivalentTo("LocalCompany", "RegionCompany", "NationalCompany");
        }

        [Fact]
        public async Task SetAllAsync_ShouldClearPreviousData()
        {
            // Arrange
            var firstData = new List<(string Advertiser, string[][] Locations)>
            {
                ("OldAdvertiser", new[] { new[] { "ru", "msk" } })
            };

            var newData = new List<(string Advertiser, string[][] Locations)>
            {
                ("NewAdvertiser", new[] { new[] { "ru", "msk" } })
            };

            await _repository.SetAllAsync(firstData);
            await _repository.SetAllAsync(newData);

            // Act
            var result = await _repository.GetAdvertisersAsync(["ru", "msk"]);

            // Assert
            result.Should().BeEquivalentTo("NewAdvertiser");
        }
    }
}
