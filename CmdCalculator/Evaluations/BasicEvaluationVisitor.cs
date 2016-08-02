using System;
using System.Collections.Generic;
using System.Linq;
using CmdCalculator.Evaluations.Exceptions;
using CmdCalculator.Interfaces.Evaluations;
using CmdCalculator.Interfaces.Expressions;

namespace CmdCalculator.Evaluations
{
    public class BasicEvaluationVisitor<T> : IEvaluationVisitor<T>
    {
        private readonly Dictionary<Type, IExpressionEvaluator<T>> _evaluatorToType;

        public BasicEvaluationVisitor(IEnumerable<IExpressionEvaluator<T>> evaluators)
        {
            _evaluatorToType = evaluators.ToDictionary(x => x.GetSupportedExpressionType());
        }
        public T Visit(IExpression expr)
        {
            if (expr == null)
            {
                throw new ArgumentNullException("expr");
            }
            IExpressionEvaluator<T> evaluator;
            if (!_evaluatorToType.TryGetValue(expr.GetType(), out evaluator))
            {
                throw new MissingEvaluatorException();
            }
            return evaluator.Evaluate(expr, this);
        }
    }
}