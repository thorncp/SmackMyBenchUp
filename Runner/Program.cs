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

            /*var results = Benchmark.Profile(10, reporter => {
                reporter.Report("hi", () => Thread.Sleep(randy.Next(50)));
                reporter.Report("yo", () => Thread.Sleep(randy.Next(50)));
            });

            foreach (var result in results)
            {
                Console.Out.WriteLine(string.Format("{0} averaged {1}ms", result.Label, result.Average()));
            }

            Console.Out.WriteLine();

            // todo: nice api for generating these run counts. hmmmm use linq with ranges?
            results = Benchmark.Profile(new[] {1, 10, 100}, reporter => {
                reporter.Report("hi", () => Thread.Sleep(randy.Next(50)));
                reporter.Report("yo", () => Thread.Sleep(randy.Next(50)));
            });

            foreach (var result in results)
            {
                Console.Out.WriteLine(string.Format("{0} averaged {1}ms on {2} runs", result.Label, result.Average(), result.RunCount));
            }*/

            Benchmark.Report(new[] {1, 10, 100}, reporter => {
                reporter.Report("hi", () => Thread.Sleep(randy.Next(50)));
                reporter.Report("yo", () => Thread.Sleep(randy.Next(50)));
            });
        }
    }
}
