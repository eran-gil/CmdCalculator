using System;
using CmdCalculator.Interfaces.Evaluations;
using CmdCalculator.Interfaces.Expressions;

namespace CmdCalculator.Evaluations
{
    public abstract class LiteralExpressionEvaluatorBase<TExp, TRes> : IExpressionEvaluator<TRes>
        where TExp : ILiteralExpression
    {
        public virtual Type GetSupportedExpressionType()
        {
            return typeof(TExp);
        }

        public virtual TRes Evaluate(IExpression expr, IEvaluationVisitor<TRes> visitor)
        {
            var expression = (TExp)expr;
            return Parse(expression.Value);
        }

        protected abstract TRes Parse(string value);
    }
}