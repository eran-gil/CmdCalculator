using System;
using CmdCalculator.Interfaces.Expressions;

namespace CmdCalculator.Evaluations
{
    public interface IExpressionEvaluator<T>
    {
        Type GetSupportedExpressionType();
        T Evaluate(IExpression expr, IExpressionVisitor<T> visitor);
    }
}