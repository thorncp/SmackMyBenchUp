using System;
using System.Linq;

namespace SmackMyBenchUp
{
    public class ConsoleReporter : Profiler
    {
        private int labelWidth;

        public override void SetUp()
        {
            labelWidth = this.Max(r => r.Label.Length) + 10;

            Console.Out.WriteLine("{0}\tTOTAL\t\tAVERAGE", (RunCount + " RUNS").PadRight(labelWidth + 2));
            Console.Out.WriteLine(string.Empty.PadRight(labelWidth, '-'));
        }

        public override void PostBenchmark(Result result)
        {
            Console.Out.WriteLine("  {0}\t{1}ms\t\t{2}ms", (result.Label + ":").PadRight(labelWidth), result.Total(), result.Average());
        }

        public override void TearDown()
        {
            Console.Out.WriteLine();
        }
    }
}
