using System;
using CmdCalculator.Interfaces.Tokens;
using CmdCalculator.Operators;
using CmdCalculator.Tokenization.Tokens;
using NUnit.Framework;

namespace CmdCalculator.Test.Tokenization
{
    [TestFixture]
    public class TokensTests
    {
        [Test, TestCaseSource(nameof(SameTokenCreatorCases))]
        public void Token_Equals_Different_Token_Representing_Same_String(Func<IToken> tokenCreator)
        {
            //Arrange
            var token1 = tokenCreator();
            var token2 = tokenCreator();

            //Act
            var areEqual = token1.Equals(token2);

            //Assert
            Assert.IsTrue(areEqual);
        }

        [Test, TestCaseSource(nameof(DifferentTokenCreatorCases))]
        public void Token_Not_Equals_Different_Token_Representing_Aמםאיקר_String(Func<IToken> token1Creator, Func<IToken> token2Creator)
        {
            //Arrange
            var token1 = token1Creator();
            var token2 = token2Creator();

            //Act
            var areEqual = token1.Equals(token2);

            //Assert
            Assert.IsFalse(areEqual);
        }

        private static readonly TestCaseData[] SameTokenCreatorCases =
        {
            new TestCaseData((Func<IToken>)(() => new LiteralToken("6")))
                .SetName("Literal token with numerical value"),
            new TestCaseData((Func<IToken>)(() => new BinaryMathOpToken<AdditionOperator>(new AdditionOperator())))
                .SetName("Binary math operator token with operator type"),
            new TestCaseData((Func<IToken>)(() => new OpenBracketsToken<OpeningBracketOperator>(new OpeningBracketOperator())))
                .SetName("Open brackets operator token with operator type"),
            new TestCaseData((Func<IToken>)(() => new CloseBracketsToken<ClosingBracketOperator>(new ClosingBracketOperator())))
                .SetName("Closing brackets operator token with operator type"),
        };

        private static readonly TestCaseData[] DifferentTokenCreatorCases =
        {
            new TestCaseData((Func<IToken>)(() => new LiteralToken("1")),
                (Func<IToken>)(() => new LiteralToken("2")))
                .SetName("2 Literal tokens with different numerical value"),
            new TestCaseData((Func<IToken>)(() => new BinaryMathOpToken<AdditionOperator>(new AdditionOperator())),
                (Func<IToken>)(() => new BinaryMathOpToken<SubtractionOperator>(new SubtractionOperator())))
                .SetName("2 Binary math operator tokens with different operator types"),
            new TestCaseData((Func<IToken>)(() => new OpenBracketsToken<OpeningBracketOperator>(new OpeningBracketOperator())),
                (Func<IToken>)(() => new OpenBracketsToken<ClosingBracketOperator>(new ClosingBracketOperator())))
                .SetName("2 Open brackets operator tokens with different operator types"),
            new TestCaseData((Func<IToken>)(() => new CloseBracketsToken<ClosingBracketOperator>(new ClosingBracketOperator())),
                (Func<IToken>)(() => new CloseBracketsToken<OpeningBracketOperator>(new OpeningBracketOperator())))
                .SetName("2 Closing brackets operator tokens with different operator types"),
        };
    }
}
