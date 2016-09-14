using CmdCalculator.Exceptions;
using CmdCalculator.Interfaces;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace CmdCalculator.Test
{
    [TestFixture]
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

        private static readonly TestCaseData[] ValidInputCases =
        {
            new TestCaseData("", 0).SetName("Empty string evaluates to 0"),
            new TestCaseData("1+1", 2).SetName("1+1 = 2"),
            new TestCaseData("1-1", 0).SetName("1-1 = 0"),
            new TestCaseData("1*1", 1).SetName("1*1 = 1"),
            new TestCaseData("1/1", 1).SetName("1/1 = 1"),
            new TestCaseData("2-2-2", -2).SetName("2-2-2 = -2"),
            new TestCaseData("4/2/2", 1).SetName("4/2/2 = 1"),
            new TestCaseData("6+(6+6)*6", 78).SetName("6+(6+6)*6 = 78"),
            new TestCaseData("(6+6)*6", 72).SetName("(6+6)*6 = 72"),
            new TestCaseData("((6+6)/2/3*6+6)*12", 216).SetName("((6+6)/2/3*6+6)*12 = 216"),
        };

        private static readonly TestCaseData[] InvalidInputCases =
        {
            new TestCaseData("(").SetName("Only one open bracket with no closing"),
            new TestCaseData(")").SetName("Only one closed bracket with no opening"),
            new TestCaseData("1-").SetName("Binary operator with only left operand"),
            new TestCaseData("-1").SetName("Binary operator with only right operand"),
            new TestCaseData("(1+)1").SetName("Closing bracket in the middle of binary operator expression"),
            new TestCaseData("(1+1))(").SetName("Same number of opening and closing brackets, but in invalid order"),
        };

    }
}
