using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CmdCalculator.Expressions;
using CmdCalculator.Extensions;
using CmdCalculator.Interfaces.Expressions;
using CmdCalculator.Interfaces.Parsers;
using CmdCalculator.Tokens;

namespace CmdCalculator.Parsers
{
    public class BinaryMathOpExpressionParser<T> : IOperatorExpressionParser
        where T : IToken
    {

        public BinaryMathOpExpressionParser(int priority)
        {
            Priority = priority;
        }

        public int Priority { get; private set; }

        public bool CanParseExpression(IEnumerable<IToken> input)
        {
            return input.OfType<T>().Any();
        }

        public IExpression ParseExpression(IEnumerable<IToken> input, Func<IEnumerable<IToken>, IExpression> operandParser)
        {
            var splitLocations = input.GetAllIndexesOf<T>().ToList();
            splitLocations.Reverse();
            IExpression expression = null;

            foreach (var splitLocation in splitLocations)
            {
                var inputParts = input.SplitAtLocation(splitLocation);
                expression = GetExpressionForParts(inputParts, operandParser);
                if (expression != null)
                {
                    break;
                }
            }

            return expression;
        }

        private IBinaryOpExpression GetExpressionForParts(IEnumerable<IEnumerable<IToken>> splittedInput,
            Func<IEnumerable<IToken>, IExpression> operandParser)
        {
            var splittedInputArr = splittedInput.ToArray();
            var firstOperand = operandParser(splittedInputArr[0]);
            var secondOperand = operandParser(splittedInputArr[1]);

            if (firstOperand == null || secondOperand == null || !IsParsedInCorrectOrder(secondOperand))
            {
                return null;
            }

            var expression = new BinaryOpExpression<T>(firstOperand, secondOperand, Priority);
            return expression;
        }

        private bool IsParsedInCorrectOrder(IExpression secondOperand)
        {
            var operatorExpression = secondOperand as IOperatorExpression;
            if (operatorExpression == null)
            {
                return true;
            }
            return operatorExpression.Priority != Priority;
        }


    }
}
