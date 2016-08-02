using CmdCalculator.Interfaces.Expressions;

namespace CmdCalculator.Interfaces.Evaluations
{
    public interface IEvaluationVisitor<out T>
    {
        T Visit(IExpression expr);
    }
}