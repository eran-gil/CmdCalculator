using CmdCalculator.Tokenization.Tokens;

namespace CmdCalculator.Evaluations
{
    class IntegerMultiplicationEvaluator : BinaryExpressionEvaluatorBase<MultiplicationToken, int>
    {
        protected override int Evaluate(int left, int right)
        {
            return left * right;
        }
    }
}