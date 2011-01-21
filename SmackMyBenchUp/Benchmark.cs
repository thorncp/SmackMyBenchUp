using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SmackMyBenchUp
{
    public static class Benchmark
    {
        public static IEnumerable<Result> Profile(int runs, Action<Bench> action)
        {
            Bench bench = new Bench();
            action(bench);

            foreach (var b in bench)
            {
                Stopwatch stopwatch = new Stopwatch();

                for (int i = 0; i < runs; i++)
                {
                    stopwatch.Start();
                    b.Action();
                    stopwatch.Stop();
                    b.RunTimes.Add(stopwatch.ElapsedMilliseconds);
                    stopwatch.Reset();
                }
            }

            return bench;
        }
    }

    public class Result
    {
        public int Runs { get; set; }
        public List<long> RunTimes { get; set; }
        public string Label { get; set; }
        public Action Action { get; set; }

        public Result()
        {
            RunTimes = new List<long>();
        }

        public double Average()
        {
            return RunTimes.Average();
        }
    }

    // todo: better name
    public class Bench : List<Result>
    {
        public void Blar(string label, Action action)
        {
            Add(new Result{ Label = label, Action = action });
        }
    }
}
