using CmdCalculator.Tokenization.Tokens;

namespace CmdCalculator.Evaluations
{
    class IntegerAdditionEvaluator : BinaryExpressionEvaluatorBase<AdditionToken,int>
    {
        protected override int Evaluate(int left, int right)
        {
            return left + right;
        }
    }
}