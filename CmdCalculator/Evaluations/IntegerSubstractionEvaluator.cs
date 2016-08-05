using CmdCalculator.Operators;

namespace CmdCalculator.Evaluations
{
    public class IntegerSubstractionEvaluator : BinaryExpressionEvaluatorBase<SubtractionOperator, int>
    {

        protected override int Evaluate(int left, int right)
        {
            return left - right;
        }
    }
}