using CmdCalculator.Tokens;

namespace CmdCalculator.Evaluations
{
    class IntegerSubstractionEvaluator : BinaryExpressionEvaluator<SubstractionToken, int>
    {
        protected override int Evaluate(int left, int right)
        {
            return left - right;
        }
    }
}