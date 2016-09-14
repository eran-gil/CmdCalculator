using CmdCalculator.Operators;

namespace CmdCalculator.Evaluations
{
    public class IntegerMultiplicationEvaluator : BinaryExpressionEvaluatorBase<MultiplicationOperator, int>
    {
        protected override int Evaluate(int left, int right)
        {
            return left * right;
        }
    }
}