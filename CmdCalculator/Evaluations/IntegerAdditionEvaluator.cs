using CmdCalculator.Operators;

namespace CmdCalculator.Evaluations
{
    public class IntegerAdditionEvaluator : BinaryExpressionEvaluatorBase<AdditionOperator, int>
    {
        protected override int Evaluate(int left, int right)
        {
            return left + right;
        }
    }
}