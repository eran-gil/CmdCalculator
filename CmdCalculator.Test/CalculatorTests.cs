using System.Linq;
using CmdCalculator.Interfaces;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace CmdCalculator.Test
{
    public class CalculatorTests
    {
        private static readonly IUnityContainer Container = CalculatorComponentsFactory.GenerateCalculatorComponentsContainer();

        [Test, TestCaseSource(nameof(CalculatorInputCases))]
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

        private static readonly object[] BasicInputCases =
        {
            new object[] {"", 0},
            new object[] {"1+1", 2},
            new object[] {"1-1", 0},
            new object[] {"1*1", 1},
            new object[] {"1/1", 1},
        };

        private static readonly object[] ComplexInputCases =
        {
            new object[] { "2-2-2", -2 },
            new object[] { "4/2/2", 1 },
            new object[] { "6+(6+6)*6", 78 },
            new object[] { "(6+6)*6", 72 },
            new object[] { "((6+6)/2/3*6+6)*12", 216},
        };

        private static readonly object[] CalculatorInputCases = BasicInputCases.Concat(ComplexInputCases).ToArray();

        
    }
}
