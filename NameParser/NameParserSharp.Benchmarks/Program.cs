using BenchmarkDotNet.Running;
using NameParserSharp.Benchmarks;

var summary = BenchmarkRunner.Run<ParseBenchmark>();
