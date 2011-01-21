using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SmackMyBenchUp
{
    public static class Benchmark
    {
        public static IEnumerable<Result> Profile(IEnumerable<int> runs, Action<Bench> action)
        {
            return runs.SelectMany(run => Profile(run, action));
        }

        public static IEnumerable<Result> Profile(int runs, Action<Bench> action)
        {
            Bench bench = new Bench(runs);
            action(bench);

            foreach (var b in bench)
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

            return bench;
        }
    }

    public class Result
    {
        public int Runs { get; set; }
        public string Label { get; set; }
        public Action Action { get; set; }
        public List<Stopwatch> Stopwatches { get; set; }

        public IEnumerable<long> RunTimes
        {
            get { return Stopwatches.Select(s => s.ElapsedMilliseconds); }
        }

        public Result()
        {
            Stopwatches = new List<Stopwatch>();
        }

        public double Average()
        {
            return RunTimes.Average();
        }
    }

    // todo: better name
    public class Bench : List<Result>
    {
        private int runs;

        public Bench(int runs)
        {
            this.runs = runs;
        }

        public void Blar(string label, Action action)
        {
            Add(new Result{ Label = label, Action = action, Runs = runs });
        }
    }
}
