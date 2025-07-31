using AdvertisingPlatform.Application.Mediator.Commands;
using AdvertisingPlatform.Application.Mediator.Handlers;
using AdvertisingPlatform.Application.Repositories.Interfaces;
using AdvertisingPlatform.Application.Utils;
using Moq;
using Xunit;

namespace AdvertisingPlatform.Tests.UnitTests.MediatorHandlers
{
    public class SetAdvertisersHandlerTests
    {
        private readonly Mock<IAdvertiserRepository> _repositoryMock = new();
        private readonly Mock<IAdvertiserParser> _parserMock = new();

        private readonly SetAdvertisersHandler _handler;

        public SetAdvertisersHandlerTests()
        {
            _handler = new SetAdvertisersHandler(_repositoryMock.Object, _parserMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Parse_File_And_Save_To_Repository()
        {
            // Arrange
            using var fakeStream = new MemoryStream();

            var parsed = new[]
            {
                ("Company 1", new[] { new[] { "ru", "moscow" } }),
                ("Company 2", new[] { new[] { "by", "gomel" } }),
            };

            _parserMock.Setup(p => p.ParseFileAsync(fakeStream, It.IsAny<CancellationToken>()))
                .ReturnsAsync(parsed);

            _repositoryMock.Setup(r => r.SetAllAsync(parsed, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var command = new SetAdvertisersCommand { FileStream = fakeStream };

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _parserMock.Verify(p => p.ParseFileAsync(fakeStream, It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(r => r.SetAllAsync(parsed, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
