using CmdCalculator.Tokenization;

namespace CmdCalculator.Test.Tokenization.Mocks
{
    public class StringInputReaderMock : StringInputReader
    {
        public StringInputReaderMock(string str) : base(str)
        {
        }

        public new int Position
        {
            get { return base.Position; }
        }
    }
}
