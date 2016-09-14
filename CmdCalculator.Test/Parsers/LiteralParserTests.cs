using System.Collections.Generic;
using CmdCalculator.Expressions;
using CmdCalculator.Interfaces;
using CmdCalculator.Interfaces.Parsers;
using CmdCalculator.Interfaces.Tokens;
using CmdCalculator.Operators;
using CmdCalculator.Parsers;
using CmdCalculator.Tokenization.Tokens;
using FakeItEasy;
using NUnit.Framework;

namespace CmdCalculator.Test.Parsers
{
    [TestFixture]
    public class LiteralParserTests
    {
        private LiteralParser _parser;

        [OneTimeSetUp]
        public void SetUpForTests()
        {
            _parser = new LiteralParser(1);
        }

        [Test, TestCaseSource(nameof(InputValidationTestCases))]
        public void Literal_Parser_Validates_Input_Correctly(IEnumerable<IToken> input, bool expected)
        {
            //Arrange
            
            //Act
            var canParse = _parser.CanParseExpression(input);

            //Assert
            Assert.AreEqual(expected, canParse);
        }

        [Test, TestCaseSource(nameof(InputParsingCases))]
        public void Literal_Parser_Parses_Input_Correctly(IEnumerable<IToken> input, LiteralExpression expected)
        {
            //Arrange
            var topParser = A.Fake<ITopExpressionParser>();
            A.CallTo(() => topParser.ParseExpression(A<IEnumerable<IToken>>.Ignored)).Returns(null);
            //Act
            var result = _parser.ParseExpression(input, topParser);

            //Assert
            Assert.IsInstanceOf<LiteralExpression>(result);
            Assert.AreEqual(expected.Value, ((LiteralExpression)result).Value);
        }

        private static readonly TestCaseData[] InputValidationTestCases = {
            new TestCaseData(new[] {new LiteralToken("6")}, true)
                .SetName("Simple literal token can be parsed"),
            new TestCaseData(new IToken[] {}, false)
                .SetName("Empty token collection cannot be parsed"),
            new TestCaseData(new IToken[] {new BinaryMathOpToken<AdditionOperator>()}, false)
                .SetName("Empty token collection cannot be parsed"),
            new TestCaseData(new[] {new LiteralToken("6"), new LiteralToken("6")}, false)
                .SetName("Two simple literal tokens cannot be parsed"),
            new TestCaseData(new IToken[] {new LiteralToken("6"), new BinaryMathOpToken<AdditionOperator>()}, false)
                .SetName("A literal token with another token cannot be parsed"),
        };

        private static readonly TestCaseData[] InputParsingCases = {
            new TestCaseData(new[] {new LiteralToken("6")}, new LiteralExpression("6"))
                .SetName("Simple literal token is parsed correctly")
        };
    }
}
