using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SmackMyBenchUp
{
    public class Result
    {
        public int RunCount { get; set; }
        public string Label { get; set; }
        public Action Action { get; set; }
        public List<Stopwatch> Stopwatches { get; set; }

        public IEnumerable<long> RunTimes
        {
            get { return Stopwatches.Select(s => s.ElapsedMilliseconds); }
        }

        public Result()
        {
            Stopwatches = new List<Stopwatch>();
        }

        public double Average()
        {
            return RunTimes.Average();
        }

        public long Total()
        {
            return RunTimes.Sum();
        }
    }
}