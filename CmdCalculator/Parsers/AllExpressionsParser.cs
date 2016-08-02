using System;
using System.Collections.Generic;
using System.Linq;
using CmdCalculator.Interfaces.Expressions;
using CmdCalculator.Interfaces.Parsers;
using CmdCalculator.Interfaces.Tokens;

namespace CmdCalculator.Parsers
{
    public class AllExpressionsParser : IExpressionParser
    {
        private readonly List<IExpressionParser> _operatorParsers;

        public AllExpressionsParser(IEnumerable<IExpressionParser> operatorParsers)
        {
            _operatorParsers = operatorParsers.OrderBy(x=>x.Priority).ToList();
        }

        public bool CanParseExpression(IEnumerable<IToken> input)
        {
            return true;
        }

        public IExpression ParseExpression(IEnumerable<IToken> input, Func<IEnumerable<IToken>, IExpression> innerExpressionParser)
        {
            return ParseExpression(input);
        }

        public int Priority
        {
            get
            {
                return int.MaxValue;
            }
        }

        private IExpression ParseExpression(IEnumerable<IToken> input)
        {
            foreach (var operatorParser in _operatorParsers)
            {
                if (!operatorParser.CanParseExpression(input))
                {
                    continue;
                }
                var parsedExpression = operatorParser.ParseExpression(input, ParseExpression);
                if (parsedExpression == null)
                {
                    continue;
                }
                return parsedExpression;
            }
            return null;
        }
    }
}
