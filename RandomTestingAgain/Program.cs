using System;
using System.Linq;
using System.Collections.Generic;
using CompiledFilters;

namespace RandomTesting
{
    internal class Program
    {
        private static bool g5(string s)
        {
            return s.Length > 5;
        }

        private static void Main(string[] args)
        {
            Filter<string> filter = new FuncFilter<string>(g5);
            var filter2 = filter & (Func<string, bool>)(s => s.Length < 10);
            var apply = filter.Compile();
            var apply2 = filter2.Compile();

            Console.WriteLine(filter.GetType());
            Console.WriteLine("a matches: " + apply("a"));
            Console.WriteLine("abcdef matched: " + apply("abcdef"));
            Console.WriteLine("abcdef matched2: " + apply2("abcdef"));
            Console.WriteLine("abcdefghijk matched2: " + apply2("abcdefghijk"));

            Console.ReadLine();
        }
    }
}
