using CmdCalculator.Interfaces.Expressions;

namespace CmdCalculator.Evaluations
{
    public interface IExpressionVisitor<out T>
    {
        T Visit(IExpression expr);
    }
}