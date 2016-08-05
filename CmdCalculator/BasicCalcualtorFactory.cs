using CmdCalculator.Interfaces;
using CmdCalculator.Interfaces.Evaluations;
using CmdCalculator.Interfaces.Parsers;
using CmdCalculator.Interfaces.Tokens;

namespace CmdCalculator
{
    public class BasicCalcualtorFactory<TInput, TOutput> : ICalcualtorFactory<TInput, TOutput>
    {
        private readonly ITokenizerFactory<TInput> _tokenizerFactory;
        private readonly IExpressionParsersProvider _expressionParsersProvider;
        private readonly IEvaluationVisitorFactory<TOutput> _visitorFactory;

        public BasicCalcualtorFactory(ITokenizerFactory<TInput> tokenizerFactory, IExpressionParsersProvider expressionParsersProvider, IEvaluationVisitorFactory<TOutput> visitorFactory)
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