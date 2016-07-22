using System;
using CmdCalculator.Exceptions;

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
