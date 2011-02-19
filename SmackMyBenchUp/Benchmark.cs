using System;
using System.Collections.Generic;

namespace SmackMyBenchUp
{
    public static class Benchmark
    {
        public static IEnumerable<Entry> Profile(Action<Profiler> definition)
        {
            var profiler = new Profiler();
            definition(profiler);
            return profiler.Execute();
        }
    }
}
