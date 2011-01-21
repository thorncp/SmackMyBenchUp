using System;
using System.Threading;
using SmackMyBenchUp;

namespace Runner
{
    public class Program
    {
        static void Main()
        {
            Random randy = new Random();

            var results = Benchmark.Profile(10, bench => {
                bench.Blar("hi", () => Thread.Sleep(randy.Next((50))));
                bench.Blar("yo", () => Thread.Sleep(randy.Next((50))));
            });

            foreach (var result in results)
            {
                Console.Out.WriteLine(string.Format("{0} averaged {1}ms", result.Label, result.Average()));
            }
        }
    }
}
