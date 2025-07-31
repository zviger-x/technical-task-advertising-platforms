using AdvertisingPlatform.Application.Mediator.Handlers;
using AdvertisingPlatform.Application.Mediator.Queries;
using AdvertisingPlatform.Application.Repositories.Interfaces;
using AdvertisingPlatform.Application.Utils;
using FluentAssertions;
using Moq;
using Xunit;

namespace AdvertisingPlatform.Tests.UnitTests.MediatorHandlers
{
    public class GetAdvertisersHandlerTests
    {
        private readonly Mock<IAdvertiserRepository> _repositoryMock;
        private readonly Mock<IAdvertiserParser> _parserMock;

        private readonly GetAdvertisersHandler _handler;

        public GetAdvertisersHandlerTests()
        {
            _repositoryMock = new();
            _parserMock = new();

            _handler = new GetAdvertisersHandler(_repositoryMock.Object, _parserMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Call_ParseLocationAsync_And_GetAdvertisersAsync_And_Return_Result()
        {
            // Arrange
            var locationPath = "/ru/svrd/revda";
            var parsedParts = new[] { "ru", "svrd", "revda" };
            var expectedAdvertisers = new[] { "Company 1", "Company 2" };

            _parserMock
                .Setup(p => p.ParseLocationAsync(locationPath, It.IsAny<CancellationToken>()))
                .ReturnsAsync(parsedParts);

            _repositoryMock
                .Setup(r => r.GetAdvertisersAsync(parsedParts, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedAdvertisers);

            var query = new GetAdvertisersQuery { LocationPath = locationPath };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedAdvertisers);

            _parserMock.Verify(p => p.ParseLocationAsync(locationPath, It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(r => r.GetAdvertisersAsync(parsedParts, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
