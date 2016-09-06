using CmdCalculator.Interfaces.Tokens;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace CmdCalculator.Test.Tokenization
{
    [TestFixture]
    public class StringInputReaderTests
    {
        private static readonly IUnityContainer Container = CalculatorComponentsFactory.GenerateCalculatorComponentsContainer();

        [Test, TestCaseSource(nameof(ValidPeeking1CharInputs))]
        public void Peeking_Returns_Expected_Char(string input, char expected)
        {
            //Arrange
            var inputReaderFactory = Container.Resolve<IInputReaderFactory<string, char>>();
            var inputReader = inputReaderFactory.Create(input);

            //Act
            var peeked = inputReader.Peek();

            //Assert
            Assert.AreEqual(expected, peeked);
        }

        [Test, TestCaseSource(nameof(ValidPeekingCharsInputs))]
        public void Peeking_Returns_Expected_Characters(string input, string expectedChars, int expectedCount)
        {
            //Arrange
            int length = expectedChars.Length;
            var inputReaderFactory = Container.Resolve<IInputReaderFactory<string, char>>();
            var inputReader = inputReaderFactory.Create(input);
            var peekingOutput = new char[length];

            //Act
            var peekedCount = inputReader.Peek(peekingOutput, length);

            //Assert
            Assert.AreEqual(expectedChars.ToCharArray(), peekingOutput);
            Assert.AreEqual(peekedCount, expectedCount);
        }

        private static readonly TestCaseData[] ValidPeeking1CharInputs =
        {
            new TestCaseData("", '\0').SetName("Empty String Peeking 1 Char"),
            new TestCaseData("H", 'H').SetName("1-Letter Upper String Peeking 1 Char"),
            new TestCaseData("h", 'h').SetName("1-Letter Lower String Peeking 1 Char"),
            new TestCaseData("Hello", 'H').SetName("5-Letter Capitalized String Peeking 1 Char"),
            new TestCaseData("hello", 'h').SetName("5-Letter Lower String Peeking 1 Char"),
            new TestCaseData("1", '1').SetName("1-Digit String Peeking 1 Char"),
            new TestCaseData("\'", '\'').SetName("1-Symbol String Peeking 1 Char"),
            new TestCaseData("(", '(').SetName("1-Operator String Peeking 1 Char"),
        };

        private static readonly TestCaseData[] ValidPeekingCharsInputs =
        {
            new TestCaseData("", "\0", 0).SetName("Empty String Peeking 1 Char"),
            new TestCaseData("H", "H", 1).SetName("1-Letter String Peeking 1 Char"),
            new TestCaseData("H", "H\0", 1).SetName("1-Letter String Peeking 2 Chars"),
            new TestCaseData("Hello", "Hello", 5).SetName("5-Letter String Peeking 5 Chars"),
            new TestCaseData("Hello", "He", 2).SetName("5-Letter String Peeking 2 Chars"),
            new TestCaseData("123", "123", 3).SetName("3-Digit String Peeking 3 Chars"),
        };
    }
}
