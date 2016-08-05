using System;
using CmdCalculator.Expressions;
using CmdCalculator.Interfaces.Evaluations;
using CmdCalculator.Interfaces.Expressions;
using CmdCalculator.Interfaces.Tokens;

namespace CmdCalculator.Evaluations
{
    public abstract class BinaryExpressionEvaluatorBase<TOp, TRes> : IExpressionEvaluator<TRes>
        where TOp : IToken
    {
        public virtual Type GetSupportedExpressionType()
        {
            return typeof (BinaryOpExpression<TOp>);
        }

        public virtual TRes Evaluate(IExpression expr, IEvaluationVisitor<TRes> visitor)
        {
            var expression = (BinaryOpExpression<TOp>)expr;
            var left = visitor.Visit(expression.FirstOperand);
            var right = visitor.Visit(expression.SecondOperand);
            return Evaluate(left, right);
        }

        protected abstract TRes Evaluate(TRes left, TRes right);
    }
}