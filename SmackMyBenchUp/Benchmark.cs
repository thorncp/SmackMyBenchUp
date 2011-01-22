using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SmackMyBenchUp
{
    public static class Benchmark
    {
        public static void Report(IEnumerable<int> runs, Action<Reporter> definition)
        {
            Report(runs, Console.Out, definition);
        }

        public static void Report(IEnumerable<int> runs, TextWriter writer, Action<Reporter> definition)
        {
            foreach (var run in runs)
            {
                Report(run, definition, writer);
            }
        }

        public static IEnumerable<Result> Report(int runs, Action<Reporter> definition, TextWriter writer)
        {
            Reporter reporter = new Reporter(runs, writer);
            definition(reporter);

            reporter.SetUp();

            foreach (var entry in reporter)
            {
                reporter.PreBenchmark(entry);

                for (int i = 0; i < runs; i++)
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    entry.Action();
                    stopwatch.Stop();
                    entry.Stopwatches.Add(stopwatch);
                }

                reporter.PostBenchmark(entry);
            }

            reporter.TearDown();

            return reporter;
        }

        public static IEnumerable<Result> Profile(IEnumerable<int> runs, Action<Profiler> definition)
        {
            return runs.SelectMany(run => Profile(run, definition)).ToList();
        }

        public static IEnumerable<Result> Profile(int runs, Action<Profiler> definition)
        {
            Profiler profiler = new Profiler(runs);
            definition(profiler);

            profiler.SetUp();

            foreach (var entry in profiler)
            {
                profiler.PreBenchmark(entry);

                for (int i = 0; i < runs; i++)
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    entry.Action();
                    stopwatch.Stop();
                    entry.Stopwatches.Add(stopwatch);
                }

                profiler.PostBenchmark(entry);
            }

            profiler.TearDown();

            return profiler;
        }
    }
}
