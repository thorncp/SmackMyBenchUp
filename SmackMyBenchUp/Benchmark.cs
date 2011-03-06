using System;

namespace SmackMyBenchUp
{
    public static class Benchmark
    {
        public static Report Profile(Action<Profiler> definition)
        {
            var profiler = new Profiler();
            definition(profiler);
            return profiler.Execute();
        }
    }
}
