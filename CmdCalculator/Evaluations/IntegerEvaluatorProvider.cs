using System.Collections.Generic;
using CmdCalculator.Interfaces.Evaluations;

namespace CmdCalculator.Evaluations
{
    public class IntegerEvaluatorProvider : IExpressionEvaluatorProvider<int>
    {
        public IEnumerable<IExpressionEvaluator<int>> Provide()
        {
            return new IExpressionEvaluator<int>[]
            {
                new BracketsEvaluator<int>(),
                new IntegerAdditionEvaluator(),
                new IntegerSubstractionEvaluator(),
                new IntegerMultiplicationEvaluator(),
                new IntegerDivisionEvaluator(),
                new IntegerLiteralExpressionEvaluator()
            };
        }
    }
}