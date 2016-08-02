using CmdCalculator.Tokens;

namespace CmdCalculator.Evaluations
{
    class IntegerDivisionEvaluator : BinaryExpressionEvaluator<DivisionToken, int>
    {
        protected override int Evaluate(int left, int right)
        {
            return left / right;
        }
    }
}