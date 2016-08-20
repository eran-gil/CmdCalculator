using System;
using CmdCalculator.Interfaces.Expressions;

namespace CmdCalculator.Interfaces.Evaluations
{
    public interface IExpressionEvaluator<T>
    {
        Type GetSupportedExpressionType();

        T Evaluate(IExpression expr, IEvaluationVisitor<T> visitor);
    }
}