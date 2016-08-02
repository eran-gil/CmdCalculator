using System;
using CmdCalculator.Expressions;
using CmdCalculator.Interfaces.Expressions;
using CmdCalculator.Tokens;

namespace CmdCalculator.Evaluations
{
    public abstract class BinaryExpressionEvaluator<TOp, TRes> : IExpressionEvaluator<TRes>
        where TOp : IToken
    {
        public virtual Type GetSupportedExpressionType()
        {
            return typeof (BinaryOpExpression<TOp>);
        }

        public virtual TRes Evaluate(IExpression expr, IExpressionVisitor<TRes> visitor)
        {
            var expression = (BinaryOpExpression<TOp>)expr;
            var left = visitor.Visit(expression.FirstOperand);
            var right = visitor.Visit(expression.SecondOperand);
            return Evaluate(left, right);
        }

        protected abstract TRes Evaluate(TRes left, TRes right);
    }

    class IntegerAdditionEvaluator : BinaryExpressionEvaluator<AdditionToken,int>
    {
        protected override int Evaluate(int left, int right)
        {
            return left + right;
        }
    }

    class IntegerSubstractionEvaluator : BinaryExpressionEvaluator<SubstractionToken, int>
    {
        protected override int Evaluate(int left, int right)
        {
            return left - right;
        }
    }

    class IntegerMultiplicationEvaluator : BinaryExpressionEvaluator<MultiplicationToken, int>
    {
        protected override int Evaluate(int left, int right)
        {
            return left * right;
        }
    }

    class IntegerDivisionEvaluator : BinaryExpressionEvaluator<DivisionToken, int>
    {
        protected override int Evaluate(int left, int right)
        {
            return left / right;
        }
    }
}