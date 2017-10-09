using CmdCalculator.Expressions;

namespace CmdCalculator.Evaluations
{
    public class IntegerLiteralExpressionEvaluator : LiteralExpressionEvaluatorBase<LiteralExpression, int>
    {
        protected override int Parse(string value)
        {
            return int.Parse(value);
        }
    }
}