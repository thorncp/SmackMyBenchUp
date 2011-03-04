using System;
using System.Diagnostics;

namespace SmackMyBenchUp
{
    public class Entry
    {
        public string Handle { get; set; }
        public Action Action { get; set; }
        public bool WarmUp { get; set; }
        public TimeSpan Elapsed { get; set; }
        public TimeSpan WarmUpElapsed { get; set; }

        public void Execute()
        {
            if (WarmUp) WarmUpElapsed = Measure();

            Elapsed = Measure();
        }

        private TimeSpan Measure()
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            Action();
            stopwatch.Stop();
            
            return stopwatch.Elapsed;
        }
    }
}