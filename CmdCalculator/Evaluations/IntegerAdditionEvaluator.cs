using CmdCalculator.Tokens;

namespace CmdCalculator.Evaluations
{
    class IntegerAdditionEvaluator : BinaryExpressionEvaluator<AdditionToken,int>
    {
        protected override int Evaluate(int left, int right)
        {
            return left + right;
        }
    }
}