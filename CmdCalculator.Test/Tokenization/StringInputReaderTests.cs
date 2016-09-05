using CmdCalculator.Interfaces.Tokens;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace CmdCalculator.Test.Tokenization
{
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

        private static readonly object[] ValidPeeking1CharInputs =
        {
            new object[] {"", '\0'},
            new object[] {"H", 'H'},
            new object[] {"h", 'h'},
            new object[] {"Hello", 'H'},
            new object[] {"hello", 'h'},
            new object[] {"1", '1'},
            new object[] {"\'", '\''},
            new object[] {"\"", '\"'},
            new object[] {"(", '('},
        };

        private static readonly object[] ValidPeekingCharsInputs =
        {
            new object[] {"", "\0", 0},
            new object[] {"H", "H", 1},
            new object[] {"H", "H\0", 1},
            new object[] {"Hello", "Hello", 5},
            new object[] {"Hello", "He", 2},
            new object[] {"123", "123", 3},
        };
    }
}
