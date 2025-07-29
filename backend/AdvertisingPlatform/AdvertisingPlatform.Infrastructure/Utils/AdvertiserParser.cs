using AdvertisingPlatform.Application.Utils;
using System.Text;

namespace AdvertisingPlatform.Infrastructure.Utils
{
    public class AdvertiserParser : IAdvertiserParser
    {
        public async Task<IEnumerable<(string Advertiser, string[][] LocationParts)>> ParseFileAsync(Stream inputStream, CancellationToken cancellationToken = default)
        {
            var results = new List<(string Advertiser, string[][] LocationParts)>();

            using var reader = new StreamReader(inputStream, Encoding.UTF8, true, leaveOpen: true);

            var line = default(string);

            while ((line = await reader.ReadLineAsync(cancellationToken)) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parsedLine = ParseLine(line.AsSpan());

                if (parsedLine != null)
                    results.Add(parsedLine.Value);
            }

            return results;
        }

        public Task<string[]> ParseLocationAsync(string locationPath, CancellationToken cancellationToken = default)
        {
            var parts = ParseLocation(locationPath.AsSpan());

            return Task.FromResult(parts);
        }

        private (string Advertiser, string[][] LocationParts)? ParseLine(ReadOnlySpan<char> line)
        {
            var colonIndex = line.IndexOf(':');
            if (colonIndex <= 0 || colonIndex == line.Length - 1)
                return null;

            var advertiserSpan = line.Slice(0, colonIndex).Trim();
            var locationsSpan = line.Slice(colonIndex + 1).Trim();

            if (advertiserSpan.IsEmpty || locationsSpan.IsEmpty)
                return null;

            var numberOfComma = locationsSpan.Count(',');
            var locationsRanges = new Span<Range>(new Range[numberOfComma + 1]);
            var locationCount = locationsSpan.Split(locationsRanges, ',', StringSplitOptions.RemoveEmptyEntries);

            if (locationCount == 0)
                return null;

            var locations = new string[locationCount][];

            for (var i = 0; i < locationCount; i++)
            {
                var locStart = locationsRanges[i].Start.Value;
                var locEnd = locationsRanges[i].End.Value;

                var current = locationsSpan.Slice(locStart, locEnd - locStart);

                locations[i] = ParseLocation(current.ToString());
            }

            return (advertiserSpan.ToString(), locations);
        }

        private string[] ParseLocation(ReadOnlySpan<char> locationPath)
        {
            var numberOfSlashes = locationPath.Count('/');
            var partsRanges = new Span<Range>(new Range[numberOfSlashes + 1]);
            var partCount = locationPath.Split(partsRanges, '/', StringSplitOptions.RemoveEmptyEntries);

            if (partCount == 0)
                return null;

            var parts = new string[partCount];

            for (var i = 0; i < partCount; i++)
            {
                var partStart = partsRanges[i].Start.Value;
                var partEnd = partsRanges[i].End.Value;

                var part = locationPath.Slice(partStart, partEnd - partStart);

                parts[i] = part.ToString();
            }

            return parts;
        }
    }
}
