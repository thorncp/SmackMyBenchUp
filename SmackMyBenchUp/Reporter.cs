using System;
using System.Collections.Generic;

namespace SmackMyBenchUp
{
    public class Reporter : List<Result>
    {
        private readonly int runCount;

        public Reporter(int runCount)
        {
            this.runCount = runCount;
        }

        public void Report(string label, Action action)
        {
            Add(new Result{ Label = label, Action = action, RunCount = runCount });
        }
    }
}
