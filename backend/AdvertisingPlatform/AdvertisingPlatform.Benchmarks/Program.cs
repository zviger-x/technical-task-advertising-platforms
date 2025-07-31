using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace AdvertisingPlatform.Benchmarks
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var config = DefaultConfig.Instance
                .WithSummaryStyle(
                    SummaryStyle.Default
                        .WithTimeUnit(Perfolizer.Horology.TimeUnit.Millisecond)
                        .WithSizeUnit(Perfolizer.Metrology.SizeUnit.KB));

            BenchmarkRunner.Run<AdvertiserParserBenchmarks>();
        }
    }
}
