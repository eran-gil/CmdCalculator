using CmdCalculator.Expressions;

namespace CmdCalculator.Evaluations
{
    class IntegerExpressionEvaluator : LiteralExpressionEvaluatorBase<LiteralExpression, int>
    {
        protected override int Parse(string value)
        {
            return int.Parse(value);
        }
    }
}