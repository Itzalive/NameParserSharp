using BenchmarkDotNet.Running;
using NameParser.Benchmarks;

var summary = BenchmarkRunner.Run<ParseBenchmark>();
