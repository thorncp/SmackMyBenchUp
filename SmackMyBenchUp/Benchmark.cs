using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SmackMyBenchUp
{
    public static class Benchmark
    {
        public static void Profile(int runs, Dictionary<string, Action> actions)
        {
            foreach (var action in actions)
            {
                Stopwatch stopwatch = new Stopwatch();

                var times = new List<long>();

                for (int i = 0; i < runs; i++)
                {
                    stopwatch.Start();
                    action.Value();
                    stopwatch.Stop();
                    times.Add(stopwatch.ElapsedMilliseconds);
                    stopwatch.Reset();
                }

                Console.Out.WriteLine(string.Format("{0} averaged {1}ms", action.Key, times.Average()));
            }
        }

        public static void Profile2(int runs, Action<Bench> action)
        {
            Bench bench = new Bench();
            action(bench);

            foreach (var b in bench)
            {
                Stopwatch stopwatch = new Stopwatch();

                var times = new List<long>();

                for (int i = 0; i < runs; i++)
                {
                    stopwatch.Start();
                    b.Value();
                    stopwatch.Stop();
                    times.Add(stopwatch.ElapsedMilliseconds);
                    stopwatch.Reset();
                }

                Console.Out.WriteLine(string.Format("{0} averaged {1}ms", b.Key, times.Average()));
            }
        }
    }

    // todo: better name
    public class Bench : Dictionary<string, Action>
    {
        public void Blar(string label, Action action)
        {
            Add(label, action);
        }
    }
}
