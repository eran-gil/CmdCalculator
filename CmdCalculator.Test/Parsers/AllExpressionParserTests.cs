using System.Collections.Generic;
using System.Linq;
using CmdCalculator.Interfaces.Expressions;
using CmdCalculator.Interfaces.Parsers;
using CmdCalculator.Interfaces.Tokens;
using CmdCalculator.Parsers;
using FakeItEasy;
using NUnit.Framework;

namespace CmdCalculator.Test.Parsers
{
    [TestFixture]
    public class AllExpressionParserTests
    {
        private IExpression _dummyExpression;

        [SetUp]
        public void SetUpTest()
        {
            _dummyExpression = A.Dummy<IExpression>();
        }

        [Test, TestCaseSource(nameof(CallsToParsersTestCases))]
        public void Parser_Attempts_Parsers_By_Priority(int numberOfParsers)
        {
            //Arrange
            var parsers = new List<IExpressionParser>();
            for (var priority = 1; priority <= numberOfParsers; priority++)
            {
                var parser = FakeDummyExpressionParser(priority);
                var shouldParse = (priority == numberOfParsers);
                A.CallTo(() => parser.CanParseExpression(A<IEnumerable<IToken>>.Ignored)).Returns(shouldParse);
                parsers.Add(parser);
            }
            var allExpressionParser = new AllExpressionsParser(parsers);

            //Act
            allExpressionParser.ParseExpression(new List<IToken>());

            //Assert
            parsers.ForEach(ValidateCallToParserCanParse);
            ValidateCallToParserParseExpression(parsers.Last());
        }

        [Test]
        public void Parser_Handles_Null_From_Parsers()
        {
            //Arrange
            var badParser = FakeAlwaysYesExpressionParser(1);
            var goodParser = FakeAlwaysYesExpressionParser(1);
            var parsers = new List<IExpressionParser> {badParser, goodParser};

            A.CallTo(() => badParser.ParseExpression(A<IEnumerable<IToken>>.Ignored, A<ITopExpressionParser>.Ignored))
                .Returns(null);
            A.CallTo(() => goodParser.ParseExpression(A<IEnumerable<IToken>>.Ignored, A<ITopExpressionParser>.Ignored))
                .Returns(_dummyExpression);

            var allExpressionParser = new AllExpressionsParser(parsers);

            //Act
            allExpressionParser.ParseExpression(new List<IToken>());

            //Assert
            parsers.ForEach(ValidateCallToParserCanParse);
            parsers.ForEach(ValidateCallToParserParseExpression);
            
        }

        private IExpressionParser FakeDummyExpressionParser(int priority)
        {
            var parser = A.Fake<IExpressionParser>();
            A.CallTo(() => parser.ParseExpression(A<IEnumerable<IToken>>.Ignored, A<ITopExpressionParser>.Ignored))
                .Returns(_dummyExpression);
            A.CallTo(() => parser.Priority).Returns(priority);
            return parser;
        }

        private IExpressionParser FakeAlwaysYesExpressionParser(int priority)
        {
            var parser = A.Fake<IExpressionParser>();
            A.CallTo(() => parser.CanParseExpression(A<IEnumerable<IToken>>.Ignored)).Returns(true);

            A.CallTo(() => parser.Priority).Returns(priority);
            return parser;
        }

        private void ValidateCallToParserCanParse(IExpressionParser parser)
        {
            A.CallTo(() => parser.CanParseExpression(A<IEnumerable<IToken>>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        private void ValidateCallToParserParseExpression(IExpressionParser parser)
        {
            A.CallTo(() => parser.ParseExpression(A<IEnumerable<IToken>>.Ignored, A<ITopExpressionParser>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        private static readonly TestCaseData[] CallsToParsersTestCases = new TestCaseData[]
        {
            new TestCaseData(1).SetName("1 parser"),
            new TestCaseData(2).SetName("2 parsers"),
            new TestCaseData(3).SetName("3 parsers"),
            new TestCaseData(4).SetName("4 parsers"),
        };
    }
}
