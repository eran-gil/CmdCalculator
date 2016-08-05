using System.Collections.Generic;

namespace CmdCalculator.Interfaces.Evaluations
{
    public interface IExpressionEvaluatorProvider<T>
    {
        IEnumerable<IExpressionEvaluator<T>> Provide();
    }
}