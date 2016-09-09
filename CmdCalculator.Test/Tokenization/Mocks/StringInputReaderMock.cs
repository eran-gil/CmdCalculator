using CmdCalculator.Tokenization;

namespace CmdCalculator.Test.Tokenization.Mocks
{
    public class StringInputReaderMock : StringInputReader
    {
        public StringInputReaderMock(string str) : base(str)
        {
        }

        public int Count
        {
            get { return _count; }
        }
    }
}
