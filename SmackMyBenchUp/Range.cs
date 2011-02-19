using System.Collections;
using System.Collections.Generic;

namespace SmackMyBenchUp
{
    public class Range : IEnumerable<int>
    {
        public int Start { get; private set; }
        public int End { get; private set; }
        public int StepSize { get; private set; }

        public Range(int start, int end, int stepSize = 1)
        {
            Start = start;
            End = end;
            StepSize = stepSize;
        }

        public IEnumerable<int> Step(int step)
        {
            for (int i = Start; i <= End; i += step)
            {
                yield return i;
            }
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = Start; i <= End; i += StepSize)
            {
                yield return i;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
