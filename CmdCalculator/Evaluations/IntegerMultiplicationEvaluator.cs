using CmdCalculator.Tokens;

namespace CmdCalculator.Evaluations
{
    class IntegerMultiplicationEvaluator : BinaryExpressionEvaluator<MultiplicationToken, int>
    {
        protected override int Evaluate(int left, int right)
        {
            return left * right;
        }
    }
}