using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SmackMyBenchUp
{
    public static class Benchmark
    {
        public static IEnumerable<Result> Profile(IEnumerable<int> runs, Action<Reporter> action)
        {
            return runs.SelectMany(run => Profile(run, action)).ToList();
        }

        public static IEnumerable<Result> Profile(int runs, Action<Reporter> action)
        {
            Reporter reporter = new Reporter(runs);
            action(reporter);

            foreach (var b in reporter)
            {
                for (int i = 0; i < runs; i++)
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    b.Action();
                    stopwatch.Stop();
                    b.Stopwatches.Add(stopwatch);
                }
            }

            return reporter;
        }
    }
}
