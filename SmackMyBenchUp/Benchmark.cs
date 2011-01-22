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
            foreach (var run in runs)
            {
                Profile(run, definition, Console.Out);
            }
        }

        public static IEnumerable<Result> Profile(IEnumerable<int> runs, Action<Reporter> definition, TextWriter writer = null)
        {
            return runs.SelectMany(run => Profile(run, definition, writer)).ToList();
        }

        public static IEnumerable<Result> Profile(int runs, Action<Reporter> definition, TextWriter writer = null)
        {
            Reporter reporter = new Reporter(runs);
            definition(reporter);

            int labelWidth = reporter.Max(r => r.Label.Length) + 10;

            if (writer != null)
            {
                writer.WriteLine("{0}\tTOTAL\t\tAVERAGE", (runs + " RUNS").PadRight(labelWidth + 2));
                writer.WriteLine(string.Empty.PadRight(labelWidth, '-'));
            }

            foreach (var entry in reporter)
            {
                for (int i = 0; i < runs; i++)
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    entry.Action();
                    stopwatch.Stop();
                    entry.Stopwatches.Add(stopwatch);
                }
                if (writer != null)
                {
                    writer.WriteLine("  {0}\t{1}ms\t\t{2}ms", (entry.Label + ":").PadRight(labelWidth), entry.Total(), entry.Average());
                }
            }

            if (writer != null)
            {
                writer.WriteLine();
            }

            return reporter;
        }
    }
}
