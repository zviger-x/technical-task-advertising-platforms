using AdvertisingPlatform.Application.Utils;
using AdvertisingPlatform.Infrastructure.Utils;
using FluentAssertions;
using System.Text;
using Xunit;

namespace AdvertisingPlatform.Tests.UnitTests.Parsers
{
    public class AdvertiserParserStackPoolTests
    {
        private readonly IAdvertiserParser _parser = new AdvertiserParserStackPool();

        [Fact]
        public async Task ParseFileAsync_Should_Parse_Valid_Single_Line()
        {
            // Arrange
            var input = "Company:/ru/svrd/revda,/by/minsk";
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(input));

            // Act
            var result = (await _parser.ParseFileAsync(stream)).ToList();

            // Assert
            result.Should().HaveCount(1);
            result[0].Advertiser.Should().Be("Company");
            result[0].LocationParts.Should().HaveCount(2);
            result[0].LocationParts[0].Should().Equal("ru", "svrd", "revda");
            result[0].LocationParts[1].Should().Equal("by", "minsk");
        }

        [Fact]
        public async Task ParseFileAsync_Should_Ignore_Empty_And_Whitespace_Lines()
        {
            // Arrange
            var input = "\n \r\n Company:/ru\n";
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(input));

            // Act
            var result = (await _parser.ParseFileAsync(stream)).ToList();

            // Assert
            result.Should().HaveCount(1);
            result[0].Advertiser.Should().Be("Company");
            result[0].LocationParts.Should().ContainSingle().Which.Should().Equal("ru");
        }

        [Fact]
        public async Task ParseFileAsync_Should_Ignore_Invalid_Lines()
        {
            // Arrange
            var input = "Company\n:onlycolon\n : \nValid:/ru";
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(input));

            // Act
            var result = (await _parser.ParseFileAsync(stream)).ToList();

            // Assert
            result.Should().HaveCount(1);
            result[0].Advertiser.Should().Be("Valid");
            result[0].LocationParts.Should().ContainSingle().Which.Should().Equal("ru");
        }

        [Fact]
        public async Task ParseLocationAsync_Should_Split_Path_Correctly()
        {
            // Act
            var result = await _parser.ParseLocationAsync("/ru/svrd/revda");

            // Assert
            result.Should().Equal("ru", "svrd", "revda");
        }

        [Fact]
        public async Task ParseLocationAsync_Should_Return_EmptyArray_For_Empty_Or_SingleSlash()
        {
            // Act
            var result1 = await _parser.ParseLocationAsync("");
            var result2 = await _parser.ParseLocationAsync("/");

            // Assert
            result1.Should().BeEmpty();
            result2.Should().BeEmpty();
        }
    }
}
