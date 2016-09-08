using System.Collections.Generic;
using CmdCalculator.Expressions;
using CmdCalculator.Interfaces.Expressions;
using CmdCalculator.Interfaces.Parsers;
using CmdCalculator.Interfaces.Tokens;
using CmdCalculator.Operators;
using CmdCalculator.Parsers;
using CmdCalculator.Tokenization.Tokens;
using FakeItEasy;
using NUnit.Framework;

namespace CmdCalculator.Test.Parsers
{
    public class BracketsExpressionParserTests
    {
        private const int ParserPriority = 1;
        private IExpressionParser _parser;

        [OneTimeSetUp]
        public void SetUpForTests()
        {
            _parser = new BracketsExpressionParser<OpenBracketsToken<OpeningBracketOperator>, CloseBracketsToken<ClosingBracketOperator>>(ParserPriority);
        }

        [Test, TestCaseSource(nameof(InputValidationTestCases))]
        public void Brackets_Parser_Validates_Input_Correctly(IEnumerable<IToken> input, bool expected)
        {
            //Arrange

            //Act
            var canParse = _parser.CanParseExpression(input);

            //Assert
            Assert.AreEqual(expected, canParse);
        }

        [Test, TestCaseSource(nameof(InputParsingCases))]
        public void Brackets_Parser_Parses_Input_Correctly(ICollection<IToken> innerTokens, IExpression expectedInnerExpression)
        {
            //Arrange
            var topParser = A.Fake<ITopExpressionParser>();
            A.CallTo(() => topParser.ParseExpression(A<IEnumerable<IToken>>.That.IsSameSequenceAs(innerTokens))).Returns(expectedInnerExpression);
            var tokensList = new List<IToken> { new OpenBracketsToken<OpeningBracketOperator>(new OpeningBracketOperator()) };
            tokensList.AddRange(innerTokens);
            tokensList.Add(new CloseBracketsToken<ClosingBracketOperator>(new ClosingBracketOperator()));

            //Act
            var result = _parser.ParseExpression(tokensList, topParser);

            //Assert
            Assert.IsInstanceOf<BracketOpExpression>(result);
            Assert.AreEqual(expectedInnerExpression, ((BracketOpExpression)result).Operand);
            A.CallTo(() => topParser.ParseExpression(A<IEnumerable<IToken>>.That.IsSameSequenceAs(innerTokens)))
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        private static readonly TestCaseData[] InputValidationTestCases = {
            new TestCaseData(new IToken[]
                {
                    new OpenBracketsToken<OpeningBracketOperator>(new OpeningBracketOperator()),
                    new LiteralToken("6"),
                    new CloseBracketsToken<ClosingBracketOperator>(new ClosingBracketOperator())
                }, true)
                .SetName("Bracketed literal expression can be parsed"),
            new TestCaseData(new IToken[]
                {
                    new OpenBracketsToken<OpeningBracketOperator>(new OpeningBracketOperator()),
                    new LiteralToken("6"),
                    new BinaryMathOpToken<AdditionOperator>(new AdditionOperator()),
                    new LiteralToken("6"),
                    new CloseBracketsToken<ClosingBracketOperator>(new ClosingBracketOperator())
                }, true)
                .SetName("Bracketed binary math op expression can be parsed"),
            new TestCaseData(new IToken[]
                {
                    new OpenBracketsToken<OpeningBracketOperator>(new OpeningBracketOperator()),
                    new OpenBracketsToken<OpeningBracketOperator>(new OpeningBracketOperator()),
                    new LiteralToken("6"),
                    new BinaryMathOpToken<AdditionOperator>(new AdditionOperator()),
                    new LiteralToken("6"),
                    new CloseBracketsToken<ClosingBracketOperator>(new ClosingBracketOperator()),
                    new CloseBracketsToken<ClosingBracketOperator>(new ClosingBracketOperator())
                }, true)
                .SetName("Nested bracketed binary math op expression can be parsed"),
            new TestCaseData(new IToken[]
                {
                    new OpenBracketsToken<OpeningBracketOperator>(new OpeningBracketOperator()),
                    new CloseBracketsToken<ClosingBracketOperator>(new ClosingBracketOperator())
                }, false)
                .SetName("Empty bracketed expression cannot be parsed"),
            new TestCaseData(new IToken[]
                {
                    new OpenBracketsToken<OpeningBracketOperator>(new OpeningBracketOperator()),
                }, false)
                .SetName("Opening bracket with no closing bracket expression cannot be parsed"),
            new TestCaseData(new IToken[]
                {
                    new CloseBracketsToken<ClosingBracketOperator>(new ClosingBracketOperator()),
                }, false)
                .SetName("Closing bracket with no opening bracket expression cannot be parsed"),
            new TestCaseData(new IToken[]
                {
                    new CloseBracketsToken<ClosingBracketOperator>(new ClosingBracketOperator()),
                    new OpenBracketsToken<OpeningBracketOperator>(new OpeningBracketOperator()),
                }, false)
                .SetName("Closing bracket before opening bracket expression cannot be parsed"),

        };

        private static readonly TestCaseData[] InputParsingCases = {
            new TestCaseData(new IToken[]
                {
                    new LiteralToken("6")
                },
                new LiteralExpression("6"))
                .SetName("Bracketed literal expression is parsed correctly"),
            new TestCaseData(new IToken[]
                {
                    new LiteralToken("6"),
                    new BinaryMathOpToken<AdditionOperator>(new AdditionOperator()),
                    new LiteralToken("6"),
                },
                new BinaryOpExpression<AdditionOperator>(new LiteralExpression("6"), new LiteralExpression("6"), 1))
                .SetName("Bracketed binary math op expression is parsed correctly"),
            new TestCaseData(new IToken[]
                {
                    new OpenBracketsToken<OpeningBracketOperator>(new OpeningBracketOperator()),
                    new LiteralToken("6"),
                    new BinaryMathOpToken<AdditionOperator>(new AdditionOperator()),
                    new LiteralToken("6"),
                    new CloseBracketsToken<ClosingBracketOperator>(new ClosingBracketOperator()),
                },
                new BracketOpExpression(new BinaryOpExpression<AdditionOperator>(new LiteralExpression("6"), new LiteralExpression("6"), 1), 1))
                .SetName("Nested bracketed binary math op expression is parsed correctly"),

        };
    }
}
