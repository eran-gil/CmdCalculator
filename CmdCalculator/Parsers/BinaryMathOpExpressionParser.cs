using System;
using System.Collections.Generic;
using System.Linq;
using CmdCalculator.Expressions;
using CmdCalculator.Extensions;
using CmdCalculator.Interfaces.Expressions;
using CmdCalculator.Interfaces.Operators;
using CmdCalculator.Interfaces.Parsers;

namespace CmdCalculator.Parsers
{
    public class BinaryMathOpExpressionParser : IOperatorExpressionParser<IBinaryOperator>
    {
        public IBinaryOperator Op { get; private set; }

        public BinaryMathOpExpressionParser(IBinaryOperator op)
        {
            Op = op;
        }

        public bool CanParseExpression(string input)
        {
            return Op.OpRegex.IsMatch(input);
        }

        public IExpression ParseExpression(string input, Func<string, IExpression> operandParser)
        {
            var splitLocations = input.GetAllIndexesOf(Op.OpString).ToList();
            splitLocations.Reverse();
            IExpression expression = null;

            foreach (var splitLocation in splitLocations)
            {
                var inputParts = input.SplitAtLocation(splitLocation);
                expression = GetExpressionForParts(inputParts, operandParser, Op);
                if (expression != null)
                {
                    break;
                }
            }

            return expression;
        }

        private IBinaryOpExpression GetExpressionForParts(IEnumerable<string> splittedInput,
            Func<string, IExpression> operandParser,
            IBinaryOperator binaryOp)
        {
            var splittedInputArr = splittedInput.ToArray();
            var firstOperand = operandParser(splittedInputArr[0]);
            var secondOperand = operandParser(splittedInputArr[1]);

            if (firstOperand == null || secondOperand == null || !IsParsedInCorrectOrder(secondOperand))
            {
                return null;
            }

            var expression = new BinaryOpExpression(firstOperand, secondOperand, binaryOp);
            return expression;
        }

        private bool IsParsedInCorrectOrder(IExpression secondOperand)
        {
            var operatorExpression = secondOperand as IOperatorExpression<IOperator>;
            if (operatorExpression == null)
            {
                return true;
            }
            return operatorExpression.Op.Priority != Op.Priority;
        }


    }
}
