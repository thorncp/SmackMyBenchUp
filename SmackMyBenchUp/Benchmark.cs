using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SmackMyBenchUp
{
    public static class Benchmark
    {
        public static IEnumerable<Result> Report(IEnumerable<int> runCounts, Action<Profiler> definition)
        {
            return Profile<ConsoleReporter>(runCounts, definition);
        }

        public static IEnumerable<Result> Report(int runCount, Action<Profiler> definition)
        {
            return Profile<ConsoleReporter>(runCount, definition);
        }

        public static IEnumerable<Result> Profile(IEnumerable<int> runCounts, Action<Profiler> definition)
        {
            return runCounts.SelectMany(run => Profile<Profiler>(run, definition));
        }

        public static IEnumerable<Result> Profile(int runCount, Action<Profiler> definition)
        {
            return Profile<Profiler>(runCount, definition);
        }

        public static IEnumerable<Result> Profile<T>(IEnumerable<int> runCounts, Action<Profiler> definition) where T : Profiler, new()
        {
            return runCounts.SelectMany(run => Profile<T>(run, definition)).ToList();
        }

        public static IEnumerable<Result> Profile<T>(int runCount, Action<Profiler> definition) where T : Profiler, new()
        {
            T profiler = new T { RunCount = runCount };
            definition(profiler);

            profiler.SetUp();

            foreach (var entry in profiler)
            {
                profiler.PreBenchmark(entry);

                for (int i = 0; i < runCount; i++)
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
