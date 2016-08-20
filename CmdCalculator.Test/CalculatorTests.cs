using System.Linq;
using CmdCalculator.Exceptions;
using CmdCalculator.Interfaces;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace CmdCalculator.Test
{
    public class CalculatorTests
    {
        private static readonly IUnityContainer Container = CalculatorComponentsFactory.GenerateCalculatorComponentsContainer();

        [Test, TestCaseSource(nameof(ValidInputCases))]
        public void Calculating_Valid_Input_Returns_Expected_Result(string input, int expected)
        {
            //Arrange
            var calculatorFactory = Container.Resolve<ICalcualtorFactory<string, int>>();
            var calculator = calculatorFactory.CreateCalculator();

            //Act
            var result = calculator.Calculate(input);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test, TestCaseSource(nameof(InvalidInputCases))]
        public void Calculating_Valid_Input_Throws_CalculatorException(string input)
        {
            //Arrange
            var calculatorFactory = Container.Resolve<ICalcualtorFactory<string, int>>();
            var calculator = calculatorFactory.CreateCalculator();

            //Act
            TestDelegate calculateDelegate = () => calculator.Calculate(input);

            //Assert
            Assert.Throws<CalculatorException>(calculateDelegate);
        }

        private static readonly object[] ValidInputCases =
        {
            new object[] { "", 0 },
            new object[] { "1+1", 2 },
            new object[] { "1-1", 0 },
            new object[] { "1*1", 1 },
            new object[] { "1/1", 1 },
            new object[] { "2-2-2", -2 },
            new object[] { "4/2/2", 1 },
            new object[] { "6+(6+6)*6", 78 },
            new object[] { "(6+6)*6", 72 },
            new object[] { "((6+6)/2/3*6+6)*12", 216 },
        };

        private static readonly object[] InvalidInputCases =
        {
            new object[] { "(" },
            new object[] { ")" },
            new object[] { "1-" },
            new object[] { "(1+)1" },
            new object[] { "(1+1))(" },
        };

    }
}
