using CmdCalculator.Tokenization.Tokens;

namespace CmdCalculator.Evaluations
{
    class IntegerSubstractionEvaluator : BinaryExpressionEvaluatorBase<SubstractionToken, int>
    {
        protected override int Evaluate(int left, int right)
        {
            return left - right;
        }
    }
}