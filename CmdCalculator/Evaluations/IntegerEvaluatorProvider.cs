using System.Collections.Generic;

namespace CmdCalculator.Evaluations
{
    class IntegerEvaluatorProvider : IExpressionEvaluatorProvider<int>
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
                new LiteralIntegerExpressionEvaluator()
            };
        }
    }
}