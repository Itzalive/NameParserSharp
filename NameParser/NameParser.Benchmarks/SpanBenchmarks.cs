using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace NameParser.Benchmarks
{
    public class SpanBenchmarks
    {
        private const string value = "the quick brown fox jumps over the lazy dog";

        private Consumer consumer = new();

        [Benchmark(Baseline = true)]
        public void LinqSplitBySpace()
        {
            consumer.Consume(value.Split(' ', StringSplitOptions.RemoveEmptyEntries));
        }

        [Benchmark]
        public void SpanSplitBySpace()
        {
            consumer.Consume(value.AsMemory().SplitToSpan(' '));
        }
    }
}
