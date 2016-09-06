using System;
using CmdCalculator.Interfaces.Operators;
using CmdCalculator.Interfaces.Tokens;
using CmdCalculator.Operators;
using CmdCalculator.Tokenization;
using CmdCalculator.Tokenization.Tokens;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace CmdCalculator.Test.Tokenization
{
    [TestFixture]
    public class ParsersTests
    {
        private static readonly IUnityContainer Container = CalculatorComponentsFactory.GenerateCalculatorComponentsContainer();

        [OneTimeSetUp]
        public void SetupTest()
        {
            RegisterTokenParsersToContainer(Container);
        }

        [Test, TestCaseSource(nameof(TestCaseDataValidTokenParsersCanReadInput))]
        public void Parser_Can_Read_Is_Correct(string input, Type parserType, bool expected)
        {
            //Arrange
            var inputReaderFactory = Container.Resolve<IInputReaderFactory<string, char>>();
            var inputReader = inputReaderFactory.Create(input);
            var parser = Container.Resolve(parserType);
            var canReadMethod = parserType.GetMethod("CanRead");

            //Act
            var canRead = (bool) (canReadMethod.Invoke(parser, new object[] { inputReader }));

            //Assert
            Assert.AreEqual(expected, canRead);
        }

        private void RegisterTokenParsersToContainer(IUnityContainer container)
        {
            var additionToken = new BinaryMathOpToken<AdditionOperator>(new AdditionOperator());
            var subtractionToken = new BinaryMathOpToken<SubtractionOperator>(new SubtractionOperator());
            var multiplicationToken = new BinaryMathOpToken<MultiplicationOperator>(new MultiplicationOperator());
            var divisionToken = new BinaryMathOpToken<DivisionOperator>(new DivisionOperator());
            var openBracketsToken = new OpenBracketsToken<OpeningBracketOperator>(new OpeningBracketOperator());
            var closingBracketsToken = new CloseBracketsToken<ClosingBracketOperator>(new ClosingBracketOperator());
            RegisterOperatorTokenParserToContainer(additionToken, container);
            RegisterOperatorTokenParserToContainer(subtractionToken, container);
            RegisterOperatorTokenParserToContainer(multiplicationToken, container);
            RegisterOperatorTokenParserToContainer(divisionToken, container);
            RegisterOperatorTokenParserToContainer(openBracketsToken, container);
            RegisterOperatorTokenParserToContainer(closingBracketsToken, container);
            container.RegisterInstance(new SpaceTokenParser());
            container.RegisterInstance(new NumberTokenParser());
        }

        private static void RegisterOperatorTokenParserToContainer<T>(IOperatorToken<T> token, IUnityContainer container)
            where T : IOperator
        {
            var parser = new OperatorTokenParser<T>(token);
            container.RegisterInstance(parser);
        }

      

        private static readonly TestCaseData[] TestCaseDataValidTokenParsersCanReadInput =
        {
            //SpaceTokenParser
            new TestCaseData(" ", typeof(SpaceTokenParser), true).SetName("Space Token Parser Test 1"),
            new TestCaseData("  ", typeof(SpaceTokenParser), true).SetName("Space Token Parser Test 2"),
            new TestCaseData(" A", typeof(SpaceTokenParser), true).SetName("Space Token Parser Test 3"),
            new TestCaseData("A", typeof(SpaceTokenParser), false).SetName("Space Token Parser Test 4"),
            new TestCaseData("A ", typeof(SpaceTokenParser), false).SetName("Space Token Parser Test 5"),
            //NumberTokenParser
            new TestCaseData("1", typeof(NumberTokenParser), true).SetName("Number Token Parser Test 1"),
            new TestCaseData("1234", typeof(NumberTokenParser), true).SetName("Number Token Parser Test 2"),
            new TestCaseData("A", typeof(NumberTokenParser), false).SetName("Number Token Parser Test 3"),
            new TestCaseData("A1", typeof(NumberTokenParser), false).SetName("Number Token Parser Test 4"),
            new TestCaseData("A 1", typeof(NumberTokenParser), false).SetName("Number Token Parser Test 5"),
            //Addition OperatorTokenParser
            new TestCaseData("+", typeof(OperatorTokenParser<AdditionOperator>), true).SetName("Addition Operator Token Parser Test 1"),
            new TestCaseData("+1", typeof(OperatorTokenParser<AdditionOperator>), true).SetName("Addition Operator Token Parser Test 2"),
            new TestCaseData("-", typeof(OperatorTokenParser<AdditionOperator>), false).SetName("Addition Operator Token Parser Test 3"),
            new TestCaseData("*", typeof(OperatorTokenParser<AdditionOperator>), false).SetName("Addition Operator Token Parser Test 4"),
            new TestCaseData("/", typeof(OperatorTokenParser<AdditionOperator>), false).SetName("Addition Operator Token Parser Test 5"),
            //Subtraction OperatorTokenParser
            new TestCaseData("-", typeof(OperatorTokenParser<SubtractionOperator>), true).SetName("Subtraction Operator Token Parser Test 1"),
            new TestCaseData("-1", typeof(OperatorTokenParser<SubtractionOperator>), true).SetName("Subtraction Operator Token Parser Test 2"),
            new TestCaseData("+", typeof(OperatorTokenParser<SubtractionOperator>), false).SetName("Subtraction Operator Token Parser Test 3"),
            new TestCaseData("*", typeof(OperatorTokenParser<SubtractionOperator>), false).SetName("Subtraction Operator Token Parser Test 4"),
            new TestCaseData("/", typeof(OperatorTokenParser<SubtractionOperator>), false).SetName("Subtraction Operator Token Parser Test 5"),
            //Multiplication OperatorTokenParser
            new TestCaseData("*", typeof(OperatorTokenParser<MultiplicationOperator>), true).SetName("Multiplication Operator Token Parser Test 1"),
            new TestCaseData("*1", typeof(OperatorTokenParser<MultiplicationOperator>), true).SetName("Multiplication Operator Token Parser Test 2"),
            new TestCaseData("+", typeof(OperatorTokenParser<MultiplicationOperator>), false).SetName("Multiplication Operator Token Parser Test 3"),
            new TestCaseData("-", typeof(OperatorTokenParser<MultiplicationOperator>), false).SetName("Multiplication Operator Token Parser Test 4"),
            new TestCaseData("/", typeof(OperatorTokenParser<MultiplicationOperator>), false).SetName("Multiplication Operator Token Parser Test 5"),
            //Division OperatorTokenParser
            new TestCaseData("/", typeof(OperatorTokenParser<DivisionOperator>), true).SetName("Division Operator Token Parser Test 1"),
            new TestCaseData("/1", typeof(OperatorTokenParser<DivisionOperator>), true).SetName("Division Operator Token Parser Test 2"),
            new TestCaseData("+", typeof(OperatorTokenParser<DivisionOperator>), false).SetName("Division Operator Token Parser Test 3"),
            new TestCaseData("-", typeof(OperatorTokenParser<DivisionOperator>), false).SetName("Division Operator Token Parser Test 4"),
            new TestCaseData("*", typeof(OperatorTokenParser<DivisionOperator>), false).SetName("Division Operator Token Parser Test 5"),
            //Opening Brackets OperatorTokenParser
            new TestCaseData("(", typeof(OperatorTokenParser<OpeningBracketOperator>), true).SetName("Opening Bracket Operator Token Parser Test 1"),
            new TestCaseData("(1+1)", typeof(OperatorTokenParser<OpeningBracketOperator>), true).SetName("Opening Bracket Operator Token Parser Test 2"),
            new TestCaseData(")", typeof(OperatorTokenParser<OpeningBracketOperator>), false).SetName("Opening Bracket Operator Token Parser Test3 "),
            new TestCaseData("+", typeof(OperatorTokenParser<OpeningBracketOperator>), false).SetName("Opening Bracket Operator Token Parser Test 4"),
            new TestCaseData("-", typeof(OperatorTokenParser<OpeningBracketOperator>), false).SetName("Opening Bracket Operator Token Parser Test 5"),
            new TestCaseData("*", typeof(OperatorTokenParser<OpeningBracketOperator>), false).SetName("Opening Bracket Operator Token Parser Test 6"),
            new TestCaseData("/", typeof(OperatorTokenParser<OpeningBracketOperator>), false).SetName("Opening Bracket Operator Token Parser Test 7"),
            //Closing Brackets OperatorTokenParser
            new TestCaseData(")", typeof(OperatorTokenParser<ClosingBracketOperator>), true).SetName("Closing Bracket Operator Token Parser Test 1"),
            new TestCaseData(")(1+1)", typeof(OperatorTokenParser<ClosingBracketOperator>), true).SetName("Closing Bracket Operator Token Parser Test 2"),
            new TestCaseData("(", typeof(OperatorTokenParser<ClosingBracketOperator>), false).SetName("Closing Bracket Operator Token Parser Test 3"),
            new TestCaseData("1)", typeof(OperatorTokenParser<ClosingBracketOperator>), false).SetName("Closing Bracket Operator Token Parser Test 4"),
            new TestCaseData("+", typeof(OperatorTokenParser<ClosingBracketOperator>), false).SetName("Closing Bracket Operator Token Parser Test 5"),
            new TestCaseData("-", typeof(OperatorTokenParser<ClosingBracketOperator>), false).SetName("Closing Bracket Operator Token Parser Test 6"),
            new TestCaseData("*", typeof(OperatorTokenParser<ClosingBracketOperator>), false).SetName("Closing Bracket Operator Token Parser Test 7"),
            new TestCaseData("/", typeof(OperatorTokenParser<ClosingBracketOperator>), false).SetName("Closing Bracket Operator Token Parser Test 8"),

        };
    }
}
