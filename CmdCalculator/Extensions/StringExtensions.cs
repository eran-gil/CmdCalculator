using System;
using System.Collections.Generic;

namespace CmdCalculator.Extensions
{
    public static class StringExtensions
    {
        public static IEnumerable<string> SplitAtLocation(this string str, int location)
        {
            var part1 = str.Substring(0, location);
            var part2 = str.Substring(location + 1);
            var parts = new List<string> { part1, part2 };
            return parts;
        }

        public static IEnumerable<int> GetAllIndexesOf(this string str, string searchString)
        {
            var indexes = new List<int>();
            for (var index = 0; ; index += searchString.Length)
            {
                index = str.IndexOf(searchString, index, StringComparison.Ordinal);
                if (index == -1)
                {
                    return indexes;
                }
                indexes.Add(index);
            }
        }


    }
}
