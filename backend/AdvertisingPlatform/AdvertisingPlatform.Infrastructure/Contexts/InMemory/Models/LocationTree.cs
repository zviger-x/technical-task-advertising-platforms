namespace AdvertisingPlatform.Infrastructure.Contexts.InMemory.Models
{
    public sealed class LocationTree
    {
        private volatile LocationNode _root = new();

        public void AddAdvertisers(IEnumerable<(string advertiser, IEnumerable<string[]> locations)> advertiserLocations)
        {
            var newRoot = new LocationNode();

            foreach (var (advertiser, paths) in advertiserLocations)
            {
                foreach (var path in paths)
                {
                    var current = newRoot;

                    foreach (var segment in path)
                    {
                        if (!current.Children.TryGetValue(segment, out var child))
                        {
                            child = new LocationNode();
                            current.Children[segment] = child;
                        }

                        current = child;
                    }

                    current.Advertisers.Add(advertiser);
                }
            }

            _root = newRoot;
        }

        public string[] FindAdvertisers(string[] location)
        {
            if (location.Length == 0)
                return Array.Empty<string>();

            var current = _root;

            var result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var segment in location)
            {
                if (!current.Children.TryGetValue(segment, out var next))
                    break;

                current = next;

                foreach (var adv in current.Advertisers)
                    result.Add(adv);
            }

            if (result.Count == 0)
                return Array.Empty<string>();

            return result.ToArray();
        }

        private sealed class LocationNode
        {
            public Dictionary<string, LocationNode> Children { get; } = new();

            public HashSet<string> Advertisers { get; } = new();
        }
    }
}
