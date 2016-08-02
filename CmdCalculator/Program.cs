using System;
using CmdCalculator.Exceptions;
using CmdCalculator.Tokens;

namespace CmdCalculator
{
    public class Program
    {
        static void Main()
        {
            var calculator = new BasicCalculator();

            while (true)
            {
                var input = Console.ReadLine();
                var tokenizedInput = new StringTokenizer(new SpaceTokenReader(),
                    new OperatorTokenReader<AdditionToken>("+"), new OperatorTokenReader<SubstractionToken>("-"),
                    new OperatorTokenReader<MultiplicationToken>("*"), new OperatorTokenReader<DivisionToken>("/"),
                    new OperatorTokenReader<OpenBracketsToken>("("), new OperatorTokenReader<CloseBracketsToken>(")"),
                    new NumberTokenReader()).Tokenize(input);
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
