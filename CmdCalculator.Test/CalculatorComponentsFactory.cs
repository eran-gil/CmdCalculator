using CmdCalculator.Evaluations;
using CmdCalculator.Interfaces;
using CmdCalculator.Interfaces.Evaluations;
using CmdCalculator.Interfaces.Parsers;
using CmdCalculator.Interfaces.Tokens;
using CmdCalculator.Parsers;
using CmdCalculator.Tokenization;
using Microsoft.Practices.Unity;

namespace CmdCalculator.Test
{
    public static class CalculatorComponentsFactory
    {
        public static IUnityContainer GenerateCalculatorComponentsContainer()
        {
            var container = new UnityContainer();

            IInputReaderFactory<string, char> stringReaderFactory = container.Resolve<StringInputReaderFactory>();
            container.RegisterInstance(stringReaderFactory);

            ITokenParsersProvider<char> tokenParsersProvider = container.Resolve<CharTokenParsersProvider>();
            container.RegisterInstance(tokenParsersProvider);

            ITokenizerFactory<string> tokenizerFactory = container.Resolve<BasicTokenizerFactory<string, char>>();
            container.RegisterInstance(tokenizerFactory);

            IExpressionParsersProvider expressionParsersProvider = container.Resolve<DefaultExpressionParsersProvider>();
            container.RegisterInstance(expressionParsersProvider);

            IExpressionEvaluatorProvider<int> evaluatorProvider = container.Resolve<IntegerEvaluatorProvider>();
            container.RegisterInstance(evaluatorProvider);

            IEvaluationVisitorFactory<int> evaluationVisitorFactory = container.Resolve<EvaluationVisitorFactory<int>>();
            container.RegisterInstance(evaluationVisitorFactory);

            ICalcualtorFactory<string, int> calculatorFactory = container.Resolve<BasicCalcualtorFactory<string, int>>();
            container.RegisterInstance(calculatorFactory);

            return container;
        }
    }
}
