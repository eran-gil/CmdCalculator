using System.Collections.Generic;
using CmdCalculator.Expressions;
using CmdCalculator.Interfaces.Expressions;
using CmdCalculator.Interfaces.Operators;
using CmdCalculator.Interfaces.Tokens;
using CmdCalculator.Operators;
using CmdCalculator.Parsers;
using CmdCalculator.Tokenization.Tokens;
using FakeItEasy;
using NUnit.Framework;

namespace CmdCalculator.Test.Parsers
{
    [TestFixture]
    public class BinaryOpExpressionParserTests
    {
        private const int ParserPriority = 1;
        private static readonly IToken AdditionToken = new BinaryMathOpToken<AdditionOperator>(new AdditionOperator());
        private static readonly IToken SubtractionToken = new BinaryMathOpToken<SubtractionOperator>(new SubtractionOperator());
        private static readonly IToken MultiplicationToken = new BinaryMathOpToken<MultiplicationOperator>(new MultiplicationOperator());
        private static readonly IToken DivisionToken = new BinaryMathOpToken<DivisionOperator>(new DivisionOperator());
        private static readonly IToken OpenBracketsToken =
            new OpenBracketsToken<OpeningBracketOperator>(new OpeningBracketOperator());
        private static readonly IToken CloseBracketsToken =
            new CloseBracketsToken<ClosingBracketOperator>(new ClosingBracketOperator());


        [Test, TestCaseSource(nameof(InputValidationTestCases))]
        public void Binary_Op_Parser_Validates_Input_Correctly<T>(IEnumerable<IToken> input, T operatorType, bool expected)
            where T : IOperator
        {
            //Arrange
            var parser = new BinaryMathOpExpressionParser<T>(ParserPriority, operatorType);

            //Act
            var canParse = parser.CanParseExpression(input);

            //Assert
            Assert.AreEqual(expected, canParse);
        }

        [Test, TestCaseSource(nameof(TokenInputForParsingCases))]
        public void Binary_Op_Parser_Passes_Input_Correctly_To_Top_Parser<T>(ICollection<IToken> leftTokens,
            ICollection<IToken> rightTokens,
            T operatorType)
            where T : IOperator
        {
            //Arrange
            var topParser = A.Fake<ITopExpressionParser>();
            var parser = new BinaryMathOpExpressionParser<T>(ParserPriority, operatorType);
            A.CallTo(() => topParser.ParseExpression(A<IEnumerable<IToken>>.Ignored)).Returns(new LiteralExpression("6"));
            var tokensList = new List<IToken>();
            tokensList.AddRange(leftTokens);
            tokensList.Add(new BinaryMathOpToken<T>(operatorType));
            tokensList.AddRange(rightTokens);

            //Act
            parser.ParseExpression(tokensList, topParser);

            //Assert
            //            Assert.IsInstanceOf<BinaryOpExpression<T>>(result);
            //            Assert.AreEqual(expectedLeftExpression, ((BinaryOpExpression<T>)result).FirstOperand);
            //            Assert.AreEqual(expectedRightExpression, ((BinaryOpExpression<T>)result).SecondOperand);
            A.CallTo(() => topParser.ParseExpression(A<IEnumerable<IToken>>.That.IsSameSequenceAs(leftTokens)))
                .MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => topParser.ParseExpression(A<IEnumerable<IToken>>.That.IsSameSequenceAs(rightTokens)))
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test, TestCaseSource(nameof(ExpresionInputForParsingCases))]
        public void Binary_Op_Parser_Uses_Valid_Expressions_From_Top_Parser_Correctly<T>(IExpression leftExpression,
            IExpression rightExpression,
            T operatorType)
            where T : IOperator
        {
            //Arrange
            var topParser = A.Fake<ITopExpressionParser>();
            var parser = new BinaryMathOpExpressionParser<T>(ParserPriority, operatorType);
            A.CallTo(() => topParser.ParseExpression(A<IEnumerable<IToken>>.That.Contains(OpenBracketsToken))).Returns(leftExpression);
            A.CallTo(() => topParser.ParseExpression(A<IEnumerable<IToken>>.That.Contains(CloseBracketsToken))).Returns(rightExpression);
            var tokensList = new List<IToken>
            {
                OpenBracketsToken,
                new BinaryMathOpToken<T>(operatorType),
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
            where T : IOperator
        {
            //Arrange
            var topParser = A.Fake<ITopExpressionParser>();
            var parser = new BinaryMathOpExpressionParser<T>(ParserPriority, operatorType);
            A.CallTo(() => topParser.ParseExpression(A<IEnumerable<IToken>>.That.Contains(OpenBracketsToken))).Returns(leftExpression);
            A.CallTo(() => topParser.ParseExpression(A<IEnumerable<IToken>>.That.Contains(CloseBracketsToken))).Returns(rightExpression);
            var tokensList = new List<IToken>
            {
                OpenBracketsToken,
                new BinaryMathOpToken<T>(operatorType),
                CloseBracketsToken
            };

            //Act
            var result = parser.ParseExpression(tokensList, topParser);

            //Assert
            Assert.IsNull(result);
        }

        private static readonly TestCaseData[] InputValidationTestCases =
        {
            new TestCaseData(new[]
            {
                AdditionToken
            }, new AdditionOperator(), true)
                .SetName("Addition operator can be parsed"),
            new TestCaseData(new[]
            {
                SubtractionToken
            }, new SubtractionOperator(), true)
                .SetName("Subtraction operator can be parsed"),
            new TestCaseData(new[]
            {
                MultiplicationToken
            }, new MultiplicationOperator(), true)
                .SetName("Multiplication operator can be parsed"),
            new TestCaseData(new[]
            {
                DivisionToken
            }, new DivisionOperator(), true)
                .SetName("Division operator can be parsed"),
            new TestCaseData(new[]
            {
                new LiteralToken("6"),
                AdditionToken,
                new LiteralToken("6"),
            }, new AdditionOperator(), true)
                .SetName("Simple addition expression can be parsed"),
            new TestCaseData(new[]
            {
                OpenBracketsToken,
                new LiteralToken("6"),
                AdditionToken,
                new LiteralToken("6"),
                CloseBracketsToken
            }, new AdditionOperator(), true)
                .SetName("Bracketed binary op expression can be parsed"),
            new TestCaseData(new[]
            {
                OpenBracketsToken,
                new LiteralToken("6"),
                CloseBracketsToken
            }, new AdditionOperator(), false)
                .SetName("Basic bracketed expression cannot be parsed"),
            new TestCaseData(new[]
            {
                new LiteralToken("6"),
            }, new AdditionOperator(), false)
                .SetName("Simple literal expression cannot be parsed"),
        };

        private static readonly TestCaseData[] TokenInputForParsingCases = {
            new TestCaseData(new IToken[]{new LiteralToken("5")},
                    new IToken[] { new LiteralToken("6") },
                    new AdditionOperator())
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
                    new MultiplicationOperator())
                .SetName("Nested binary op expression is parsed correctly"),
        };

        private static readonly TestCaseData[] ExpresionInputForParsingCases = {
            new TestCaseData(
                new LiteralExpression("6"),
                new LiteralExpression("6"),
                new AdditionOperator())
                .SetName("Simple binary op expression is parsed correctly"),
            new TestCaseData(
                new BinaryOpExpression<AdditionOperator>(
                    new LiteralExpression("6"),
                    new LiteralExpression("6"), 1),
                new BinaryOpExpression<AdditionOperator>(
                    new LiteralExpression("6"),
                    new LiteralExpression("6"), 2),
                new MultiplicationOperator())
                .SetName("Nested binary op expression is parsed correctly"),
        };

        private static readonly TestCaseData[] InvalidExpressionInputForParsingCases = {
            new TestCaseData(
                new LiteralExpression("6"),
                null,
                new AdditionOperator())
                .SetName("Right expression cannot be parsed and so is the whole expression"),
            new TestCaseData(
                null,
                new LiteralExpression("6"),
                new AdditionOperator())
                .SetName("Left expression cannot be parsed and so is the whole expression"),
            new TestCaseData(
                new BinaryOpExpression<AdditionOperator>(
                    new LiteralExpression("6"),
                    new LiteralExpression("6"), 2),
                new BinaryOpExpression<AdditionOperator>(
                    new LiteralExpression("6"),
                    new LiteralExpression("6"), ParserPriority),
                new MultiplicationOperator())
                .SetName("Right operand is the same priority as the operand, and thus cannot be parsed this way"),
        };

    }
}
