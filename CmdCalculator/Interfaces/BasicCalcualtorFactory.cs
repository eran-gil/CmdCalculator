using CmdCalculator.Evaluations;
using CmdCalculator.Tokens;

namespace CmdCalculator.Interfaces
{
    public class BasicCalcualtorFactory<TInput, TOutput> : ICalcualtorFactory<TInput, TOutput>
    {
        private readonly ITokenizerFactory<TInput> _tokenizerFactory;
        private readonly IExpressionParsersProvider _expressionParsersProvider;
        private readonly IExpressionVisitorFactory<TOutput> _visitorFactory;

        public BasicCalcualtorFactory(ITokenizerFactory<TInput> tokenizerFactory, IExpressionParsersProvider expressionParsersProvider, IExpressionVisitorFactory<TOutput> visitorFactory)
        {
            _tokenizerFactory = tokenizerFactory;
            _expressionParsersProvider = expressionParsersProvider;
            _visitorFactory = visitorFactory;
        }

        public ICalculator<TInput, TOutput> CreateCalculator()
        {
            return new BasicCalculator<TInput, TOutput>(_tokenizerFactory.Create(), _visitorFactory.Create(), _expressionParsersProvider.Provide());
        }
    }
}