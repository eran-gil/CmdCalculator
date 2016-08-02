using System;
using System.Collections.Generic;
using System.Linq;
using CmdCalculator.Expressions;
using CmdCalculator.Interfaces.Expressions;
using CmdCalculator.Interfaces.Parsers;
using CmdCalculator.Tokens;

namespace CmdCalculator.Parsers
{
    public class LiteralParser : IOperatorExpressionParser
    {
        public LiteralParser(int priority)
        {
            Priority = priority;
        }

        public int Priority { get; private set; }

        public bool CanParseExpression(IEnumerable<IToken> input)
        {
            var inputArr = input.Take(2).ToArray();
            if (inputArr.Length == 1 && inputArr[0] is LiteralToken)
            {
                return true;
            }
            return false;
        }

        public IExpression ParseExpression(IEnumerable<IToken> input, Func<IEnumerable<IToken>, IExpression> innerExpressionParser)
        {
            var literalValue = input.Cast<LiteralToken>().First().Value;
            return new LiteralExpression(literalValue);
        }

    }
}
