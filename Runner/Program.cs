using System;
using System.Collections.Generic;
using SmackMyBenchUp;

namespace Runner
{
    public class Program
    {
        static void Main(string[] args)
        {
            Benchmark.Profile(10, new Dictionary<string, Action> {
                { "hi", () => { int x = 42; } },
                { "yo", () => { int x = 1337; } }
            });

            Benchmark.Profile2(10, bench => {
                bench.Blar("hi", () => { int x = 42; });
                bench.Blar("yo", () => { int x = 1337; });
            });
        }
    }
}
