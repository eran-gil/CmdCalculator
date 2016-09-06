using System.Collections.Generic;
using CmdCalculator.Interfaces.Tokens;
using CmdCalculator.Operators;
using CmdCalculator.Tokenization.Exceptions;
using CmdCalculator.Tokenization.Tokens;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace CmdCalculator.Test.Tokenization
{
    [TestFixture]
    public class TokenizationTest
    {
        private static readonly IUnityContainer Container = CalculatorComponentsFactory.GenerateCalculatorComponentsContainer();


        [Test, TestCaseSource(nameof(ValidInputTestCases))]
        public void Tokenizing_Valid_Input_Returns_Expected_Tokens(string input, IEnumerable<IToken> expected)
        {
            //Arrange
            var tokenizerFactory = Container.Resolve<ITokenizerFactory<string>>();
            var tokenizer = tokenizerFactory.Create();
            
            //Act
            var tokens = tokenizer.Tokenize(input);

            //Assert
            Assert.AreEqual(expected, tokens);
        }

        [Test, TestCaseSource(nameof(InvalidInputTestCases))]
        public void Tokenizing_Invalid_Input_Throws_Missing_Reader_Exception(string input, IEnumerable<IToken> expected)
        {
            //Arrange
            var tokenizerFactory = Container.Resolve<ITokenizerFactory<string>>();
            var tokenizer = tokenizerFactory.Create();

            //Act
            TestDelegate tokenizeDelegate = () => tokenizer.Tokenize(input);

            //Assert
            Assert.Throws<MissingReaderException>(tokenizeDelegate);
        }

        private static readonly TestCaseData[] ValidInputTestCases =
        {
            new TestCaseData("", new IToken[] {}).SetName("Empty string returns empty token"),
            new TestCaseData("+", new IToken[] {new BinaryMathOpToken<AdditionOperator>(new AdditionOperator()), }).SetName("Plus character is parsed to addition token"),
            new TestCaseData("-", new IToken[] {new BinaryMathOpToken<SubtractionOperator>(new SubtractionOperator()), }).SetName("Minus character is parsed to subtraction token"),
            new TestCaseData("*", new IToken[] {new BinaryMathOpToken<MultiplicationOperator>(new MultiplicationOperator()), }).SetName("Multiply character is parsed to multiplication token"),
            new TestCaseData("/", new IToken[] {new BinaryMathOpToken<DivisionOperator>(new DivisionOperator()), }).SetName("Divide character is parsed to division token"),
            new TestCaseData("(", new IToken[] {new OpenBracketsToken<OpeningBracketOperator>(new OpeningBracketOperator()) }).SetName("Opening bracket character is parsed to opening bracket token"),
            new TestCaseData(")", new IToken[] {new CloseBracketsToken<ClosingBracketOperator>(new ClosingBracketOperator()) }).SetName("Closing bracket character is parsed to closing bracket token"),
            new TestCaseData("1", new IToken[] {new LiteralToken("1") }).SetName("Digit character is parsed to literal token"),
            new TestCaseData("(1+1)",
                new IToken[] {
                    new OpenBracketsToken<OpeningBracketOperator>(new OpeningBracketOperator()) ,
                    new LiteralToken("1"),
                    new BinaryMathOpToken<AdditionOperator>(new AdditionOperator()),
                    new LiteralToken("1"),
                    new CloseBracketsToken<ClosingBracketOperator>(new ClosingBracketOperator())
                }
            ).SetName("Mathematical expression is parsed correctly in order to correct tokens"),
        };

        private static readonly TestCaseData[] InvalidInputTestCases =
        {
            new TestCaseData("[", new IToken[] {}).SetName("Unknown character cannot be parsed 1"),
            new TestCaseData("]", new IToken[] {}).SetName("Unknown character cannot be parsed 2"),
            new TestCaseData(";", new IToken[] {}).SetName("Unknown character cannot be parsed 3"),
            new TestCaseData("56.5", new IToken[] {}).SetName("Unknown character with other known characters cannot be parsed"),
        };

    }
}
