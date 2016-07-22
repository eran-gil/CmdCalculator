using System;
using CmdCalculator.Expressions;
using CmdCalculator.Interfaces.Expressions;
using CmdCalculator.Interfaces.Operators;
using CmdCalculator.Interfaces.Parsers;

namespace CmdCalculator.Parsers
{
    public class BracketsExpressionParser : IOperatorExpressionParser<IUnaryOperator>
    {
        public IUnaryOperator Op { get; }


        public BracketsExpressionParser(IUnaryOperator op)
        {
            Op = op;
        }

        public bool CanParseExpression(string input)
        {
            return Op.OpRegex.Match(input).Success && IsWholeExpressionInBrackets(input);
        }

        public IExpression ParseExpression(string input, Func<string, IExpression> operandParser)
        {
            var innerExpressionStr = input.Substring(1, input.Length - 2);
            var innerExpression = operandParser(innerExpressionStr);
            var bracketsExpression = new UnaryOpExpression(innerExpression, Op);
            return bracketsExpression;
        }

        private bool IsWholeExpressionInBrackets(string input)
        {
            var openBrackets = 0;
            for (var i = 0; i < input.Length; i++)
            {
                var character = input[i];

                if (character == '(')
                {
                    openBrackets++;
                }
                else if (character == ')')
                {
                    openBrackets--;
                }

                if (openBrackets == 0 && i < input.Length - 1)
                {
                    return false;
                }
            }

            return openBrackets == 0;
        }
    }
}
