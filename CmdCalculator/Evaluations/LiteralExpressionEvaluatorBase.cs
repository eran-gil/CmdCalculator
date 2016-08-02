using System;
using CmdCalculator.Interfaces.Evaluations;
using CmdCalculator.Interfaces.Expressions;

namespace CmdCalculator.Evaluations
{
    public abstract class LiteralExpressionEvaluatorBase<TOp, TRes> : IExpressionEvaluator<TRes>
        where TOp : ILiteralExpression
    {
        public virtual Type GetSupportedExpressionType()
        {
            return typeof(TOp);
        }

        public virtual TRes Evaluate(IExpression expr, IEvaluationVisitor<TRes> visitor)
        {
            var expression = (TOp)expr;
            return Parse(expression.Value);
        }

        protected abstract TRes Parse(string value);
    }
}