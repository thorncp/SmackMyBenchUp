using System;
using System.Collections.Generic;

namespace SmackMyBenchUp
{
    public class Profiler
    {
        private readonly List<Entry> entries = new List<Entry>();
        public bool WarmUp { get; set; }

        public IEnumerable<Entry> Execute()
        {
            foreach (var entry in entries)
            {
                entry.Execute();
            }
			
			return entries;
        }

        public void Profile(string handle, Action action)
        {
            entries.Add(new Entry {
                Handle = handle,
                Action = action,
                WarmUp = WarmUp
            });
        }
    }
}
