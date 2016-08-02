using CmdCalculator.Interfaces.Evaluations;

namespace CmdCalculator.Evaluations
{
    class EvaluationVisitorFactory<T> : IEvaluationVisitorFactory<T>
    {
        private readonly IExpressionEvaluatorProvider<T> _provider;

        public EvaluationVisitorFactory(IExpressionEvaluatorProvider<T> provider)
        {
            _provider = provider;
        }

        public IEvaluationVisitor<T> Create()
        {
            return new BasicEvaluationVisitor<T>(_provider.Provide());
        }
    }
}