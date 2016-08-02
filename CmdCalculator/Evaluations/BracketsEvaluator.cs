using CmdCalculator.Expressions;

namespace CmdCalculator.Evaluations
{
    class BracketsEvaluator<T> : UnaryExpressionEvaluator<BracketOpExpression, T>
    {
        protected override T Evaluate(T innerExpression)
        {
            return innerExpression;
        }
    }
}