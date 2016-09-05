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

        [Test, TestCaseSource(nameof(ValidTokenParsersCanReadInput))]
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

        private static readonly object[] ValidTokenParsersCanReadInput =
        {
            //SpaceTokenParser
            new object[] {" ", typeof(SpaceTokenParser), true},
            new object[] {"  ", typeof(SpaceTokenParser), true},
            new object[] {" A", typeof(SpaceTokenParser), true},
            new object[] {"A", typeof(SpaceTokenParser), false},
            new object[] {"A ", typeof(SpaceTokenParser), false},
            //NumberTokenParser
            new object[] {"1", typeof(NumberTokenParser), true},
            new object[] {"1234", typeof(NumberTokenParser), true},
            new object[] {"A", typeof(NumberTokenParser), false},
            new object[] {"A1", typeof(NumberTokenParser), false},
            new object[] {"A 1", typeof(NumberTokenParser), false},
            //Addition OperatorTokenParser
            new object[] {"+", typeof(OperatorTokenParser<AdditionOperator>), true},
            new object[] {"+1", typeof(OperatorTokenParser<AdditionOperator>), true},
            new object[] {"-", typeof(OperatorTokenParser<AdditionOperator>), false},
            new object[] {"*", typeof(OperatorTokenParser<AdditionOperator>), false},
            new object[] {"/", typeof(OperatorTokenParser<AdditionOperator>), false},
            //Subtraction OperatorTokenParser
            new object[] {"-", typeof(OperatorTokenParser<SubtractionOperator>), true},
            new object[] {"-1", typeof(OperatorTokenParser<SubtractionOperator>), true},
            new object[] {"+", typeof(OperatorTokenParser<SubtractionOperator>), false},
            new object[] {"*", typeof(OperatorTokenParser<SubtractionOperator>), false},
            new object[] {"/", typeof(OperatorTokenParser<SubtractionOperator>), false},
            //Multiplication OperatorTokenParser
            new object[] {"*", typeof(OperatorTokenParser<MultiplicationOperator>), true},
            new object[] {"*1", typeof(OperatorTokenParser<MultiplicationOperator>), true},
            new object[] {"+", typeof(OperatorTokenParser<MultiplicationOperator>), false},
            new object[] {"-", typeof(OperatorTokenParser<MultiplicationOperator>), false},
            new object[] {"/", typeof(OperatorTokenParser<MultiplicationOperator>), false},
            //Division OperatorTokenParser
            new object[] {"/", typeof(OperatorTokenParser<DivisionOperator>), true},
            new object[] {"/1", typeof(OperatorTokenParser<DivisionOperator>), true},
            new object[] {"+", typeof(OperatorTokenParser<DivisionOperator>), false},
            new object[] {"-", typeof(OperatorTokenParser<DivisionOperator>), false},
            new object[] {"*", typeof(OperatorTokenParser<DivisionOperator>), false},
            //Opening Brackets OperatorTokenParser
            new object[] {"(", typeof(OperatorTokenParser<OpeningBracketOperator>), true},
            new object[] {"(1+1)", typeof(OperatorTokenParser<OpeningBracketOperator>), true},
            new object[] {")", typeof(OperatorTokenParser<OpeningBracketOperator>), false},
            new object[] {"+", typeof(OperatorTokenParser<OpeningBracketOperator>), false},
            new object[] {"-", typeof(OperatorTokenParser<OpeningBracketOperator>), false},
            new object[] {"*", typeof(OperatorTokenParser<OpeningBracketOperator>), false},
            new object[] {"/", typeof(OperatorTokenParser<OpeningBracketOperator>), false},
            //Closing Brackets OperatorTokenParser
            new object[] {")", typeof(OperatorTokenParser<ClosingBracketOperator>), true},
            new object[] {")(1+1)", typeof(OperatorTokenParser<ClosingBracketOperator>), true},
            new object[] {"(", typeof(OperatorTokenParser<ClosingBracketOperator>), false},
            new object[] {"1)", typeof(OperatorTokenParser<ClosingBracketOperator>), false},
            new object[] {"+", typeof(OperatorTokenParser<ClosingBracketOperator>), false},
            new object[] {"-", typeof(OperatorTokenParser<ClosingBracketOperator>), false},
            new object[] {"*", typeof(OperatorTokenParser<ClosingBracketOperator>), false},
            new object[] {"/", typeof(OperatorTokenParser<ClosingBracketOperator>), false},

        };
    }
}
