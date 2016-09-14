using System.Collections.Generic;
using System.Linq;
using CmdCalculator.Interfaces.Expressions;
using CmdCalculator.Interfaces.Parsers;
using CmdCalculator.Interfaces.Tokens;

namespace CmdCalculator.Parsers
{
    public class AllExpressionsParser : ITopExpressionParser
    {
        private readonly List<IExpressionParser> _operatorParsers;

        public AllExpressionsParser(IEnumerable<IExpressionParser> operatorParsers)
        {
            _operatorParsers = operatorParsers.OrderBy(x => x.Priority).ToList();
        }

        public IExpression ParseExpression(IEnumerable<IToken> input)
        {
            var inputArray = input.ToArray();
            foreach (var operatorParser in _operatorParsers)
            {
                if (!operatorParser.CanParseExpression(inputArray))
                {
                    continue;
                }
                var parsedExpression = operatorParser.ParseExpression(inputArray, this);
                if (parsedExpression == null)
                {
                    continue;
                }
                return parsedExpression;
            }
            return null;
            // Linq version:
            // return _operatorParsers.Where(x => x.CanParseExpression(input))
            //         .Select(x => x.ParseExpression(input, ParseExpression))
            //         .FirstOrDefault(x => x != null);
        }
    }
}
