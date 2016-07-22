using System;
using CmdCalculator.Expressions;
using CmdCalculator.Interfaces.Expressions;
using CmdCalculator.Interfaces.Operators;
using CmdCalculator.Interfaces.Parsers;
using static System.Int32;

namespace CmdCalculator.Parsers
{
    public class LiteralParser : IOperatorExpressionParser <IOperator>
    {
        public LiteralParser(IOperator op)
        {
            Op = op;
        }

        public bool CanParseExpression(string input)
        {
            return Op.OpRegex.IsMatch(input);
        }

        public IExpression ParseExpression(string input, Func<string, IExpression> innerExpressionParser)
        {
            var literalValue = Parse(input);
            return new LiteralExpression(literalValue);
        }

        public IOperator Op { get; }
    }
}
