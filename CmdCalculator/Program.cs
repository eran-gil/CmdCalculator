using System;
using CmdCalculator.Evaluations;
using CmdCalculator.Exceptions;
using CmdCalculator.Interfaces.Evaluations;
using CmdCalculator.Interfaces.Parsers;
using CmdCalculator.Interfaces.Tokens;
using CmdCalculator.Parsers;
using CmdCalculator.Tokenization;
using Microsoft.Practices.Unity;

namespace CmdCalculator
{
    public class Program
    {
        static void Main()
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

            var calculatorFactory = container.Resolve<BasicCalcualtorFactory<string, int>>();

            var calculator = calculatorFactory.CreateCalculator();

            while (true)
            {
                Console.WriteLine("Please enter an expression for the calculator");
                var input = Console.ReadLine();
                if (input == "exit")
                {
                    break;
                }

                int result;
                try
                {
                    result = calculator.Calculate(input);
                }
                catch (CalculatorException ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }

                Console.WriteLine(result);
                Console.WriteLine();
            }
        }
    }
}
