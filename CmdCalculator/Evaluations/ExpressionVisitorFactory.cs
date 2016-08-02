namespace CmdCalculator.Evaluations
{
    class ExpressionVisitorFactory<T> : IExpressionVisitorFactory<T>
    {
        private readonly IExpressionEvaluatorProvider<T> _provider;

        public ExpressionVisitorFactory(IExpressionEvaluatorProvider<T> provider)
        {
            _provider = provider;
        }

        public IExpressionVisitor<T> Create()
        {
            return new CalculatorExpressionVisitor<T>(_provider.Provide());
        }
    }
}