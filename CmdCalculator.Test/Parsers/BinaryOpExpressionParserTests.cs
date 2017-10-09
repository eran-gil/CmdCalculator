using System.Collections.Generic;
using System.Linq;
using CmdCalculator.Expressions;
using CmdCalculator.Interfaces.Expressions;
using CmdCalculator.Interfaces.Operators;
using CmdCalculator.Interfaces.Parsers;
using CmdCalculator.Interfaces.Tokens;
using CmdCalculator.Operators;
using CmdCalculator.Parsers;
using CmdCalculator.Tokenization.Tokens;
using NSubstitute;
using NUnit.Framework;

namespace CmdCalculator.Test.Parsers
{
    [TestFixture]
    public class BinaryOpExpressionParserTests
    {
        private const int ParserPriority = 1;
        private static readonly IToken AdditionToken = new BinaryMathOpToken<AdditionOperator>();
        private static readonly IToken SubtractionToken = new BinaryMathOpToken<SubtractionOperator>();
        private static readonly IToken MultiplicationToken = new BinaryMathOpToken<MultiplicationOperator>();
        private static readonly IToken DivisionToken = new BinaryMathOpToken<DivisionOperator>();
        private static readonly IToken OpenBracketsToken =
            new OpenBracketsToken<OpeningBracketOperator>();
        private static readonly IToken CloseBracketsToken =
            new CloseBracketsToken<ClosingBracketOperator>();

        [Test, TestCaseSource(nameof(InputValidationTestCases))]
        public void Binary_Op_Parser_Validates_Input_Correctly<T>(IEnumerable<IToken> input, T operatorType, bool expected)
            where T : IOperator, new()

        {
            //Arrange
            var parser = new BinaryMathOpExpressionParser<T>(ParserPriority);

            //Act
            var canParse = parser.CanParseExpression(input);

            //Assert
            Assert.AreEqual(expected, canParse);
        }

        [Test, TestCaseSource(nameof(TokenInputForParsingCases))]
        public void Binary_Op_Parser_Passes_Input_Correctly_To_Top_Parser<T>(ICollection<IToken> leftTokens,
            ICollection<IToken> rightTokens,
            T operatorType)
            where T : IOperator, new()
        {
            //Arrange
            var topParser = Substitute.For<ITopExpressionParser>();
            var parser = new BinaryMathOpExpressionParser<T>(ParserPriority);
            topParser.ParseExpression(null).ReturnsForAnyArgs(new LiteralExpression("6"));
            var tokensList = new List<IToken>();
            tokensList.AddRange(leftTokens);
            tokensList.Add(new BinaryMathOpToken<T>());
            tokensList.AddRange(rightTokens);

            //Act
            parser.ParseExpression(tokensList, topParser);

            //Assert
            topParser.Received(1).ParseExpression(Arg.Is<IEnumerable<IToken>>(tokens => tokens.SequenceEqual(leftTokens)));
            topParser.Received(1).ParseExpression(Arg.Is<IEnumerable<IToken>>(tokens => tokens.SequenceEqual(rightTokens)));
        }

        [Test, TestCaseSource(nameof(ExpresionInputForParsingCases))]
        public void Binary_Op_Parser_Uses_Valid_Expressions_From_Top_Parser_Correctly<T>(IExpression leftExpression,
            IExpression rightExpression,
            T operatorType)
            where T : IOperator, new()
        {
            //Arrange
            var topParser = Substitute.For<ITopExpressionParser>();
            var parser = new BinaryMathOpExpressionParser<T>(ParserPriority);
            topParser.ParseExpression(Arg.Is<IEnumerable<IToken>>(tokens => tokens.Contains(OpenBracketsToken))).Returns(leftExpression);
            topParser.ParseExpression(Arg.Is<IEnumerable<IToken>>(tokens => tokens.Contains(CloseBracketsToken))).Returns(rightExpression);
            var tokensList = new List<IToken>
            {
                OpenBracketsToken,
                new BinaryMathOpToken<T>(),
                CloseBracketsToken
            };

            //Act
            var result = parser.ParseExpression(tokensList, topParser);

            //Assert
            Assert.IsInstanceOf<BinaryOpExpression<T>>(result);
            Assert.AreEqual(leftExpression, ((BinaryOpExpression<T>)result).FirstOperand);
            Assert.AreEqual(rightExpression, ((BinaryOpExpression<T>)result).SecondOperand);
        }
        [Test, TestCaseSource(nameof(InvalidExpressionInputForParsingCases))]
        public void Binary_Op_Parser_Returns_Null_When_Input_Is_Invalid<T>(IExpression leftExpression,
            IExpression rightExpression, T operatorType)
            where T : IOperator, new()
        {
            //Arrange
            var topParser = Substitute.For<ITopExpressionParser>();
            var parser = new BinaryMathOpExpressionParser<T>(ParserPriority);
            topParser.ParseExpression(Arg.Is<IEnumerable<IToken>>(tokens => tokens.Contains(OpenBracketsToken))).Returns(leftExpression);
            topParser.ParseExpression(Arg.Is<IEnumerable<IToken>>(tokens => tokens.Contains(CloseBracketsToken))).Returns(rightExpression);
            var tokensList = new List<IToken>
            {
                OpenBracketsToken,
                new BinaryMathOpToken<T>(),
                CloseBracketsToken
            };

            //Act
            var result = parser.ParseExpression(tokensList, topParser);

            //Assert
            Assert.IsNull(result);
        }

        private static readonly AdditionOperator AdditionOperator = new AdditionOperator();
        private static readonly SubtractionOperator SubtractionOperator = new SubtractionOperator();
        private static readonly MultiplicationOperator MultiplicationOperator = new MultiplicationOperator();
        private static readonly DivisionOperator DivisionOperator = new DivisionOperator();

        private static readonly TestCaseData[] InputValidationTestCases =
        {
            new TestCaseData(new[]
            {
                AdditionToken
            }, AdditionOperator, true)
                .SetName("Addition operator can be parsed"),
            new TestCaseData(new[]
            {
                SubtractionToken
            }, SubtractionOperator, true)
                .SetName("Subtraction operator can be parsed"),
            new TestCaseData(new[]
            {
                MultiplicationToken
            }, MultiplicationOperator, true)
                .SetName("Multiplication operator can be parsed"),
            new TestCaseData(new[]
            {
                DivisionToken
            }, DivisionOperator, true)
                .SetName("Division operator can be parsed"),
            new TestCaseData(new[]
            {
                new LiteralToken("6"),
                AdditionToken,
                new LiteralToken("6"),
            }, AdditionOperator, true)
                .SetName("Simple addition expression can be parsed"),
            new TestCaseData(new[]
            {
                OpenBracketsToken,
                new LiteralToken("6"),
                AdditionToken,
                new LiteralToken("6"),
                CloseBracketsToken
            }, AdditionOperator, true)
                .SetName("Bracketed binary op expression can be parsed"),
            new TestCaseData(new[]
            {
                OpenBracketsToken,
                new LiteralToken("6"),
                CloseBracketsToken
            }, AdditionOperator, false)
                .SetName("Basic bracketed expression cannot be parsed"),
            new TestCaseData(new[]
            {
                new LiteralToken("6"),
            }, AdditionOperator, false)
                .SetName("Simple literal expression cannot be parsed"),
        };

        private static readonly TestCaseData[] TokenInputForParsingCases = {
            new TestCaseData(new IToken[]{new LiteralToken("5")},
                    new IToken[] { new LiteralToken("6") },
                    AdditionOperator)
                .SetName("Simple binary op expression is parsed correctly"),
            new TestCaseData(
                new[]{
                    OpenBracketsToken,
                    new LiteralToken("5"),
                    AdditionToken,
                    new LiteralToken("5"),
                    CloseBracketsToken,
                },
                new[] {
                    OpenBracketsToken,
                    new LiteralToken("6"),
                    AdditionToken,
                    new LiteralToken("6"),
                    CloseBracketsToken },
                    MultiplicationOperator)
                .SetName("Nested binary op expression is parsed correctly"),
        };

        private static readonly TestCaseData[] ExpresionInputForParsingCases = {
            new TestCaseData(
                new LiteralExpression("6"),
                new LiteralExpression("6"),
                AdditionOperator)
                .SetName("Simple binary op expression is parsed correctly"),
            new TestCaseData(
                new BinaryOpExpression<AdditionOperator>(
                    new LiteralExpression("6"),
                    new LiteralExpression("6"), 1),
                new BinaryOpExpression<AdditionOperator>(
                    new LiteralExpression("6"),
                    new LiteralExpression("6"), 2),
                MultiplicationOperator)
                .SetName("Nested binary op expression is parsed correctly"),
        };

        private static readonly TestCaseData[] InvalidExpressionInputForParsingCases = {
            new TestCaseData(
                new LiteralExpression("6"),
                null,
                AdditionOperator)
                .SetName("Right expression cannot be parsed and so is the whole expression"),
            new TestCaseData(
                null,
                new LiteralExpression("6"),
                AdditionOperator)
                .SetName("Left expression cannot be parsed and so is the whole expression"),
            new TestCaseData(
                new BinaryOpExpression<AdditionOperator>(
                    new LiteralExpression("6"),
                    new LiteralExpression("6"), 2),
                new BinaryOpExpression<AdditionOperator>(
                    new LiteralExpression("6"),
                    new LiteralExpression("6"), ParserPriority),
                MultiplicationOperator)
                .SetName("Right operand is the same priority as the operand, and thus cannot be parsed this way"),
        };

    }
}
