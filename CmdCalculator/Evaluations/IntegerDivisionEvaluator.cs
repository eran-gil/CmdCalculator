using CmdCalculator.Operators;

namespace CmdCalculator.Evaluations
{
    public class IntegerDivisionEvaluator : BinaryExpressionEvaluatorBase<DivisionOperator, int>
    {

        protected override int Evaluate(int left, int right)
        {
            return left / right;
        }
    }
}