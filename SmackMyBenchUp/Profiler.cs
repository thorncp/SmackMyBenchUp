using System;

namespace SmackMyBenchUp
{
    public class Profiler
    {
        public bool WarmUp { get; set; }
        private readonly Report report = new Report();

        public Report Execute()
        {
            foreach (var entry in report)
            {
                entry.Execute();
            }

            return report;
        }

        public void Profile(string handle, Action action)
        {
            report.Entries.Add(new Entry {
                Handle = handle,
                Action = action,
                WarmUp = WarmUp
            });
        }
    }
}
