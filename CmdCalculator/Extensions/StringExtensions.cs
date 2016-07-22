using System;
using System.Collections.Generic;
using System.Linq;

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
            var matchLength = searchString.Length;

            var firstMatch = str.IndexOf(searchString, StringComparison.Ordinal);

            if (firstMatch < 0)
            {
                return null;
            }
            indexes.Add(firstMatch);

            if (firstMatch + matchLength == str.Length - 1)
            {
                return indexes;
            }

            var stringAfterMatch = str.Substring(firstMatch + matchLength);
            var indexesAfterMatch = stringAfterMatch.GetAllIndexesOf(searchString);

            if (indexesAfterMatch == null)
            {
                return indexes;
            }

            var updatedIndexesAfterMatch = indexesAfterMatch.Select(match => match + firstMatch + 1);
            indexes.AddRange(updatedIndexesAfterMatch);
            return indexes;
        }


    }
}
