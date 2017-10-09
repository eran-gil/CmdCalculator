using System.Collections.Generic;
using System.Linq;
using CmdCalculator.Extensions;
using CmdCalculator.Interfaces.Tokens;
using NSubstitute;
using NUnit.Framework;

namespace CmdCalculator.Test.Extensions
{
    public class TokenCollectionExtensionsTests
    {
        [Test, TestCaseSource(nameof(CollectionSplitTestCases))]
        public void CollectionIsSplitCorrectly(ICollection<IToken> before, ICollection<IToken> after)
        {
            //Arrange
            var collection = new List<IToken>(before) {Substitute.For<IToken>()};
            collection.AddRange(after);
            var position = before.Count();

            //Act
            var result = collection.SplitAtLocation(position).ToArray();

            //Assert
            Assert.AreEqual(before, result[0]);
            Assert.AreEqual(after, result[1]);
        }

        [Test, TestCaseSource(nameof(CollectionSearchTestCases))]
        public void ExtensionFindsAllInstancesInCollection(ICollection<IToken> tokens, IToken tokenToFind,
            ICollection<int> expectedIndexes)
        {
            //Arrange

            //Act
            var result = tokens.GetAllIndexesOf(tokenToFind);

            //Assert
            Assert.AreEqual(expectedIndexes, result);
        }

        private static readonly IToken DummyToken1 = Substitute.For<IToken>();
        private static readonly IToken DummyToken2 = Substitute.For<IToken>();

        private static readonly TestCaseData[] CollectionSplitTestCases =
        {
            new TestCaseData(
                new [] {DummyToken1},
                new [] {DummyToken2}
                ).SetName("2 Single item collections"),
            new TestCaseData(
                new [] {DummyToken1, DummyToken1},
                new [] {DummyToken2, DummyToken2}
                ).SetName("2 Multi item collections"),
            new TestCaseData(
                new [] {DummyToken1, DummyToken1, DummyToken1},
                new [] {DummyToken2, DummyToken1}
                ).SetName("2 Collections with different lengths"),
        };

        private static readonly TestCaseData[] CollectionSearchTestCases =
        {
            new TestCaseData(
                new [] {DummyToken1, DummyToken2},
                DummyToken1,
                new[] {0}
                ).SetName("Single occurence of the wanted token"),
            new TestCaseData(
                new [] {DummyToken1, DummyToken2, DummyToken1},
                DummyToken1,
                new[] {0, 2}
                ).SetName("Multiple occurences of the wanted token"),
            new TestCaseData(
                new [] {DummyToken2, DummyToken1, DummyToken2},
                DummyToken1,
                new[] {1}
                ).SetName("Multiple occurences of the non-wanted token"),
        };
    }
}
