using System.Collections.Generic;

namespace CmdCalculator.Evaluations
{
    public interface IExpressionEvaluatorProvider<T>
    {
        IEnumerable<IExpressionEvaluator<T>> Provide();
    }
}