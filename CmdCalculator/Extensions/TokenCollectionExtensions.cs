using System.Collections.Generic;
using System.Linq;
using CmdCalculator.Interfaces.Tokens;

namespace CmdCalculator.Extensions
{
    public static class TokenCollectionExtensions
    {
        public static IEnumerable<IToken[]> SplitAtLocation(this IEnumerable<IToken> str, int location)
        {
            var part1 = str.Take(location).ToArray();
            var part2 = str.Skip(location + 1).ToArray();
            var parts = new List<IToken[]> { part1, part2 };
            return parts;
        }

        public static IEnumerable<int> GetAllIndexesOf<T>(this IEnumerable<IToken> str)
        {
            int count = 0;
            foreach (var token in str)
            {
                if (token is T)
                {
                    yield return count;
                }
                count++;
            }
        }


    }
}
