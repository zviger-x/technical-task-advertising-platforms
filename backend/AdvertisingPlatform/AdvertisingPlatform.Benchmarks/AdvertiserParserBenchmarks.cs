using AdvertisingPlatform.Application.Utils;
using AdvertisingPlatform.Infrastructure.Utils;
using BenchmarkDotNet.Attributes;
using System.Text;

namespace AdvertisingPlatform.Benchmarks
{
    [MemoryDiagnoser]
    public class AdvertiserParserBenchmarks
    {
        [Params(100)]
        public int RepeatCount;

        private const int MaxSegments = 256;

        private readonly IAdvertiserParser _heapParser = new AdvertiserParserStackHeap();
        private readonly IAdvertiserParser _poolParser = new AdvertiserParserStackPool();

        private string _shortLocation;
        private string _longLocation;

        private Stream _shortStream;
        private Stream _longStream;

        [GlobalSetup]
        public void Setup()
        {
            _shortLocation = "/" + string.Join('/', Enumerable.Range(0, 3).Select(i => $"seg{i}"));
            _longLocation = "/" + string.Join('/', Enumerable.Range(0, MaxSegments).Select(i => $"seg{i}"));

            var shortLines = new[]
            {
                $"Small1:{_shortLocation}",
                $"Small2:{_shortLocation}",
                $"Small3:{_shortLocation}",
            };

            var longLines = new[]
            {
                $"Huge1:{_longLocation}",
                $"Huge2:{_longLocation}",
                $"Huge3:{_longLocation}",
            };

            _shortStream = BuildStream(shortLines);
            _longStream = BuildStream(longLines);
        }

        private static Stream BuildStream(string[] lines)
        {
            var text = string.Join('\n', lines);

            return new MemoryStream(Encoding.UTF8.GetBytes(text));
        }

        [Benchmark]
        public async Task ShortLocation_Heap()
        {
            for (int i = 0; i < RepeatCount; i++)
                await _heapParser.ParseLocationAsync(_shortLocation);
        }

        [Benchmark]
        public async Task ShortLocation_Pool()
        {
            for (int i = 0; i < RepeatCount; i++)
                await _poolParser.ParseLocationAsync(_shortLocation);
        }

        [Benchmark]
        public async Task LongLocation_Heap()
        {
            for (int i = 0; i < RepeatCount; i++)
                await _heapParser.ParseLocationAsync(_longLocation);
        }

        [Benchmark]
        public async Task LongLocation_Pool()
        {
            for (int i = 0; i < RepeatCount; i++)
                await _poolParser.ParseLocationAsync(_longLocation);
        }

        [Benchmark]
        public async Task ShortFile_Heap()
        {
            for (int i = 0; i < RepeatCount; i++)
            {
                _shortStream.Position = 0;
                await _heapParser.ParseFileAsync(_shortStream);
            }
        }

        [Benchmark]
        public async Task ShortFile_Pool()
        {
            for (int i = 0; i < RepeatCount; i++)
            {
                _shortStream.Position = 0;
                await _poolParser.ParseFileAsync(_shortStream);
            }
        }

        [Benchmark]
        public async Task LongFile_Heap()
        {
            for (int i = 0; i < RepeatCount; i++)
            {
                _longStream.Position = 0;
                await _heapParser.ParseFileAsync(_longStream);
            }
        }

        [Benchmark]
        public async Task LongFile_Pool()
        {
            for (int i = 0; i < RepeatCount; i++)
            {
                _longStream.Position = 0;
                await _poolParser.ParseFileAsync(_longStream);
            }
        }
    }
}
