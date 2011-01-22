using System.IO;
using System.Linq;

namespace SmackMyBenchUp
{
    public class Reporter : Profiler
    {
        protected readonly TextWriter Writer;
        private int labelWidth;

        public Reporter(int runCount, TextWriter writer) : base(runCount)
        {
            Writer = writer;
        }

        public override void SetUp()
        {
            labelWidth = this.Max(r => r.Label.Length) + 10;

            Writer.WriteLine("{0}\tTOTAL\t\tAVERAGE", (RunCount + " RUNS").PadRight(labelWidth + 2));
            Writer.WriteLine(string.Empty.PadRight(labelWidth, '-'));
        }

        public override void PostBenchmark(Result result)
        {
            Writer.WriteLine("  {0}\t{1}ms\t\t{2}ms", (result.Label + ":").PadRight(labelWidth), result.Total(), result.Average());
        }

        public override void TearDown()
        {
            Writer.WriteLine();
        }
    }
}
