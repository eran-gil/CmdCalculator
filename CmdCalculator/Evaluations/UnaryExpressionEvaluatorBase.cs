using System;
using CmdCalculator.Interfaces.Evaluations;
using CmdCalculator.Interfaces.Expressions;

namespace CmdCalculator.Evaluations
{
    public abstract class UnaryExpressionEvaluatorBase<TOp, TRes> : IExpressionEvaluator<TRes>
        where TOp : IUnaryOpExpression
    {
        public virtual Type GetSupportedExpressionType()
        {
            return typeof(TOp);
        }

        public virtual TRes Evaluate(IExpression expr, IEvaluationVisitor<TRes> visitor)
        {
            var expression = (TOp)expr;
            var operand = visitor.Visit(expression.Operand);
            return Evaluate(operand);
        }

        protected abstract TRes Evaluate(TRes operand);
    }
}