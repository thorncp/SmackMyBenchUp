using System;
using System.Diagnostics;

namespace SmackMyBenchUp
{
    public class Entry
    {
        public string Handle { get; set; }
        public Action Action { get; set; }
        public TimeSpan Elapsed { get; set; }

        public void Execute(bool warmUp = false)
        {
            if (warmUp) Action();

            var stopwatch = new Stopwatch();

            stopwatch.Start();
            Action();
            stopwatch.Stop();

            Elapsed = stopwatch.Elapsed;
        }
    }
}