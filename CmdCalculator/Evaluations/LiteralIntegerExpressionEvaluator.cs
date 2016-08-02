using CmdCalculator.Expressions;

namespace CmdCalculator.Evaluations
{
    class LiteralIntegerExpressionEvaluator : LiteralExpressionEvaluator<LiteralExpression, int>
    {
        protected override int Parse(string value)
        {
            return int.Parse(value);
        }
    }
}