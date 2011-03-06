using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmackMyBenchUp;

namespace Runner
{
    public class Program
    {
        static void Main()
        {
            var range = new Range(2000, 10000);

            Report report = Benchmark.Profile(profiler => {
                profiler.WarmUp = true;

                foreach (int i in range.Step(2000))
                {
                    var list = Generate(i);
                    profiler.Profile("linq: " + i, () => list.LinqJoin(","));
                    profiler.Profile("loop: " + i, () => list.Join(","));
                }
            });

            Console.Out.WriteLine("Warm Up");

            foreach (var result in report)
            {
                Console.Out.WriteLine("{0} - {1}: ", result.Handle, result.WarmUpElapsed);
            }

            Console.Out.WriteLine();
            Console.Out.WriteLine("Real Deal");

            foreach (var result in report)
            {
                Console.Out.WriteLine("{0} - {1}: ", result.Handle, result.Elapsed);
            }
        }

        public static List<string> Generate(int size)
        {
            var list = new List<string>();

            for (int i = 0; i < size; i++)
            {
                list.Add(Guid.NewGuid().ToString());
            }

            return list;
        }
    }

    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Joins the string representation of each element using the given separator, which defaults to an empty string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="separator">The separator.</param>
        /// <returns></returns>
        public static string Join<T>(this IEnumerable<T> source, string separator = "")
        {
            var str = new StringBuilder();
            foreach (var element in source)
            {
                str.Append(element);
                str.Append(separator); 
            }

            str.Remove(str.Length - separator.Length, separator.Length);

            return str.ToString();
        }

        /// <summary>
        /// Joins the string representation of each element using the given separator, which defaults to an empty string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="separator">The separator.</param>
        /// <returns></returns>
        public static string LinqJoin<T>(this IEnumerable<T> source, string separator = "")
        {
            return source.IsEmpty() ? null : source.Select(t => t.ToString()).Aggregate((join, next) => join + separator + next);
        }

        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }
    }
}
