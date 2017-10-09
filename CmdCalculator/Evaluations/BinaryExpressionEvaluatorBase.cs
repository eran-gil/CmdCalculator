using System;
using CmdCalculator.Expressions;
using CmdCalculator.Interfaces.Evaluations;
using CmdCalculator.Interfaces.Expressions;
using CmdCalculator.Interfaces.Operators;

namespace CmdCalculator.Evaluations
{
    public abstract class BinaryExpressionEvaluatorBase<TOp, TRes> : IExpressionEvaluator<TRes>
        where TOp : IOperator
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