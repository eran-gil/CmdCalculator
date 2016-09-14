using System.Collections.Generic;
using CmdCalculator.Expressions;
using CmdCalculator.Interfaces;
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
            var tokensList = new List<IToken> { new OpenBracketsToken<OpeningBracketOperator>() };
            tokensList.AddRange(innerTokens);
            tokensList.Add(new CloseBracketsToken<ClosingBracketOperator>());

            //Act
            var result = _parser.ParseExpression(tokensList, topParser);

            //Assert
            Assert.IsInstanceOf<BracketOpExpression>(result);
            Assert.AreEqual(expectedInnerExpression, ((BracketOpExpression)result).Operand);
            A.CallTo(() => topParser.ParseExpression(A<IEnumerable<IToken>>.That.IsSameSequenceAs(innerTokens)))
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        private static readonly BinaryMathOpToken<AdditionOperator> AdditionToken = new BinaryMathOpToken<AdditionOperator>();
        private static readonly OpenBracketsToken<OpeningBracketOperator> OpenBracketsToken =
            new OpenBracketsToken<OpeningBracketOperator>();
        private static readonly CloseBracketsToken<ClosingBracketOperator> CloseBracketsToken =
            new CloseBracketsToken<ClosingBracketOperator>();

        private static readonly TestCaseData[] InputValidationTestCases = {
            new TestCaseData(new IToken[]
                {
                    new OpenBracketsToken<OpeningBracketOperator>(),
                    new LiteralToken("6"),
                    new CloseBracketsToken<ClosingBracketOperator>()
                }, true)
                .SetName("Bracketed literal expression can be parsed"),
            new TestCaseData(new IToken[]
                {
                    OpenBracketsToken,
                    new LiteralToken("6"),
                    AdditionToken,
                    new LiteralToken("6"),
                    CloseBracketsToken
                }, true)
                .SetName("Bracketed binary math op expression can be parsed"),
            new TestCaseData(new IToken[]
                {
                    OpenBracketsToken,
                    OpenBracketsToken,
                    new LiteralToken("6"),
                    AdditionToken,
                    new LiteralToken("6"),
                    CloseBracketsToken,
                    CloseBracketsToken
                }, true)
                .SetName("Nested bracketed binary math op expression can be parsed"),
            new TestCaseData(new IToken[]
                {
                    OpenBracketsToken,
                    CloseBracketsToken
                }, false)
                .SetName("Empty bracketed expression cannot be parsed"),
            new TestCaseData(new IToken[]
                {
                    OpenBracketsToken
                }, false)
                .SetName("Opening bracket with no closing bracket expression cannot be parsed"),
            new TestCaseData(new IToken[]
                {
                    CloseBracketsToken
                }, false)
                .SetName("Closing bracket with no opening bracket expression cannot be parsed"),
            new TestCaseData(new IToken[]
                {
                    CloseBracketsToken,
                    OpenBracketsToken
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
                    AdditionToken,
                    new LiteralToken("6"),
                },
                new BinaryOpExpression<AdditionOperator>(new LiteralExpression("6"), new LiteralExpression("6"), 1))
                .SetName("Bracketed binary math op expression is parsed correctly"),
            new TestCaseData(new IToken[]
                {
                    OpenBracketsToken,
                    new LiteralToken("6"),
                    AdditionToken,
                    new LiteralToken("6"),
                    CloseBracketsToken
                },
                new BracketOpExpression(new BinaryOpExpression<AdditionOperator>(new LiteralExpression("6"), new LiteralExpression("6"), 1), 1))
                .SetName("Nested bracketed binary math op expression is parsed correctly"),

        };
    }
}
