using System.Collections.Generic;
using System.Linq;

namespace CmdCalculator.Tokens
{
    public class StringTokenizer : ITokenizer<string>
    {
        private List<ITokenReader<char>> _readers;

        public StringTokenizer(params ITokenReader<char>[] readers)
            : this((IEnumerable<ITokenReader<char>>)readers)
        {

        }

        public StringTokenizer(IEnumerable<ITokenReader<char>> readers)
        {
            _readers = readers.ToList();
        }

        public IToken[] Tokenize(string input)
        {
            List<IToken> tokens = new List<IToken>();
            var inputReader = new StringInputReader(input);
            while (inputReader.Peek() != '\0')
            {
                var tokenReader = GetTokenReader(inputReader);
                var token = tokenReader.ReadToken(inputReader);
                if (token != null)
                {
                    tokens.Add(token);
                }
            }

            return tokens.ToArray();
        }

        private ITokenReader<char> GetTokenReader(IInputPeeker<char> peeker)
        {
            return _readers.First(x => x.CanRead(peeker));
        }
    }
}