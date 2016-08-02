using System;
using CmdCalculator.Evaluations;
using CmdCalculator.Exceptions;
using CmdCalculator.Interfaces;
using CmdCalculator.Tokens;

namespace CmdCalculator
{
    public class Program
    {
        static void Main()
        {
            var calculatorFactory =
                new BasicCalcualtorFactory<string, int>(
                    new BasicTokenizerFactory<string, char>(
                        new StringInputReaderFactory(),
                        new CharTokenParsersProvider()
                    ),
                    new DefaultExpressionParsersProvider(), 
                    new ExpressionVisitorFactory<int>(
                        new IntegerEvaluatorProvider()
                    )
                );

            var calculator = calculatorFactory.CreateCalculator();

            while (true)
            {
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
            }
        }
    }
}
