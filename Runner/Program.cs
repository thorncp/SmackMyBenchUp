using System;
using SmackMyBenchUp;

namespace Runner
{
    public class Program
    {
        static void Main()
        {
            var results = Benchmark.Profile(10, bench => {
                bench.Blar("hi", () => { int x = 42; });
                bench.Blar("yo", () => { int x = 1337; });
            });

            foreach (var result in results)
            {
                Console.Out.WriteLine(string.Format("{0} averaged {1}ms", result.Label, result.Average()));
            }
        }
    }
}
