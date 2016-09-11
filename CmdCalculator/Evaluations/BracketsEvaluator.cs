using CmdCalculator.Expressions;

namespace CmdCalculator.Evaluations
{
    public class BracketsEvaluator<T> : UnaryExpressionEvaluatorBase<BracketOpExpression, T>
    {
        protected override T Evaluate(T operand)
        {
            return operand;
        }
    }
}