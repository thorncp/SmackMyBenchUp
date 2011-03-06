using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SmackMyBenchUp
{
    public class Report : IEnumerable<Entry>
    {
        public List<Entry> Entries { get; set; }

        public Report ()
        {
            Entries = new List<Entry>();
        }

        public IEnumerator<Entry> GetEnumerator()
        {
            return Entries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool WarmUp()
        {
            return Entries.Any(e => e.WarmUp);
        }
    }
}
