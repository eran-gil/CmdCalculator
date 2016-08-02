using CmdCalculator.Tokenization.Tokens;

namespace CmdCalculator.Evaluations
{
    class IntegerDivisionEvaluator : BinaryExpressionEvaluatorBase<DivisionToken, int>
    {
        protected override int Evaluate(int left, int right)
        {
            return left / right;
        }
    }
}