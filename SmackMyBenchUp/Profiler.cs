using System;
using System.Collections.Generic;

namespace SmackMyBenchUp
{
    public class Profiler : List<Result>
    {
        protected readonly int RunCount;

        public Profiler(int runCount)
        {
            RunCount = runCount;
        }

        public void Report(string label, Action action)
        {
            Add(new Result{ Label = label, Action = action, RunCount = RunCount });
        }

        public virtual void SetUp() {}
        public virtual void TearDown() {}
        public virtual void PreBenchmark(Result result) {}
        public virtual void PostBenchmark(Result result) {}
    }
}
