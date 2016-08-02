using System;
using System.Collections.Generic;
using System.Linq;

namespace CmdCalculator.Tokens
{
    public class BasicTokenizer<TInput, TParserOutput> : ITokenizer<TInput> 
        where TParserOutput : IEquatable<TParserOutput>
    {
        private readonly IInputReaderFactory<TInput, TParserOutput> _readerFactory;
        private readonly List<ITokenParser<TParserOutput>> _parsers;

        public BasicTokenizer(IInputReaderFactory<TInput, TParserOutput> readerFactory, IEnumerable<ITokenParser<TParserOutput>> parsers)
        {
            _readerFactory = readerFactory;
            _parsers = parsers.ToList();
        }

        public IToken[] Tokenize(TInput input)
        {
            List<IToken> tokens = new List<IToken>();
            var inputReader = _readerFactory.Create(input);
            while (!inputReader.EmptyValue.Equals(inputReader.Peek()))
            {
                var tokenReader = GetTokenParser(inputReader);
                var token = tokenReader.ReadToken(inputReader);
                if (token != null)
                {
                    tokens.Add(token);
                }
            }

            return tokens.ToArray();
        }

        private ITokenParser<TParserOutput> GetTokenParser(IInputPeeker<TParserOutput> peeker)
        {
            var reader =  _parsers.FirstOrDefault(x => x.CanRead(peeker));
            if (reader == null)
            {
                throw new MissingReaderException();
            }
            return reader;
        }
    }
}