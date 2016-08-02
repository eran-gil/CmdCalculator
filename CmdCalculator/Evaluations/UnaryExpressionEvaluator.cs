﻿using System;
using CmdCalculator.Interfaces.Expressions;

namespace CmdCalculator.Evaluations
{
    public abstract class UnaryExpressionEvaluator<TOp, TRes> : IExpressionEvaluator<TRes>
        where TOp : IUnaryOpExpression
    {
        public virtual Type GetSupportedExpressionType()
        {
            return typeof(TOp);
        }

        public virtual TRes Evaluate(IExpression expr, IExpressionVisitor<TRes> visitor)
        {
            var expression = (TOp)expr;
            var innerExpression = visitor.Visit(expression.Operand);
            return Evaluate(innerExpression);
        }

        protected abstract TRes Evaluate(TRes innerExpression);
    }
}