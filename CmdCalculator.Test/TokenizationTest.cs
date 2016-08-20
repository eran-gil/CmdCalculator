using System.Collections.Generic;
using CmdCalculator.Interfaces.Tokens;
using CmdCalculator.Operators;
using CmdCalculator.Tokenization.Exceptions;
using CmdCalculator.Tokenization.Tokens;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace CmdCalculator.Test
{
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
        public void Tokenizing_Invalid_Input_Returns_Expected_Tokens(string input, IEnumerable<IToken> expected)
        {
            //Arrange
            var tokenizerFactory = Container.Resolve<ITokenizerFactory<string>>();
            var tokenizer = tokenizerFactory.Create();

            //Act
            TestDelegate tokenizeDelegate = () => tokenizer.Tokenize(input);

            //Assert
            Assert.Throws<MissingReaderException>(tokenizeDelegate);
        }

        private static readonly object[] ValidInputTestCases =
        {
            new object[] {"", new IToken[] {}},
            new object[] {"+", new IToken[] {new BinaryMathOpToken<AdditionOperator>(new AdditionOperator()), }},
            new object[] {"-", new IToken[] {new BinaryMathOpToken<SubtractionOperator>(new SubtractionOperator()), }},
            new object[] {"*", new IToken[] {new BinaryMathOpToken<MultiplicationOperator>(new MultiplicationOperator()), }},
            new object[] {"/", new IToken[] {new BinaryMathOpToken<DivisionOperator>(new DivisionOperator()), }},
            new object[] {"(", new IToken[] {new OpenBracketsToken<OpeningBracketOperator>(new OpeningBracketOperator()) }},
            new object[] {")", new IToken[] {new CloseBracketsToken<ClosingBracketOperator>(new ClosingBracketOperator()) }},
            new object[] {"1", new IToken[] {new LiteralToken("1") }},
            new object[] {"(1+1)",
                new IToken[] {
                    new OpenBracketsToken<OpeningBracketOperator>(new OpeningBracketOperator()) ,
                    new LiteralToken("1"),
                    new BinaryMathOpToken<AdditionOperator>(new AdditionOperator()),
                    new LiteralToken("1"),
                    new CloseBracketsToken<ClosingBracketOperator>(new ClosingBracketOperator())
                }
            },
        };

        private static readonly object[] InvalidInputTestCases =
        {
            new object[] {"[", new IToken[] {}},
            new object[] {"]", new IToken[] {}},
            new object[] {";", new IToken[] {}},
            new object[] {"56.5", new IToken[] {}},
            new object[] {".", new IToken[] {}},
            new object[] {"x", new IToken[] {}},
            new object[] {"y", new IToken[] {}},
            new object[] {"z", new IToken[] {}},
        };

    }
}
