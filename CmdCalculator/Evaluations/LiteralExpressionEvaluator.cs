using System;
using CmdCalculator.Expressions;
using CmdCalculator.Interfaces.Expressions;

namespace CmdCalculator.Evaluations
{
    public abstract class LiteralExpressionEvaluator<TOp, TRes> : IExpressionEvaluator<TRes>
        where TOp : ILiteralExpression
    {
        public virtual Type GetSupportedExpressionType()
        {
            return typeof(TOp);
        }

        public virtual TRes Evaluate(IExpression expr, IExpressionVisitor<TRes> visitor)
        {
            var expression = (TOp)expr;
            return Parse(expression.Value);
        }

        protected abstract TRes Parse(string value);
    }

    class LiteralIntegerExpressionEvaluator : LiteralExpressionEvaluator<LiteralExpression, int>
    {
        protected override int Parse(string value)
        {
            return int.Parse(value);
        }
    }
}