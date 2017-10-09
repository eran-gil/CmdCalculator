using System;
using CmdCalculator.Interfaces.Tokens;

namespace CmdCalculator.Tokenization
{
    public class BasicTokenizerFactory<TInput, TParserOutput> : ITokenizerFactory<TInput>
        where TParserOutput : IEquatable<TParserOutput>
    {
        private readonly IInputReaderFactory<TInput, TParserOutput> _inputReaderFactory;
        private readonly ITokenParsersProvider<TParserOutput> _parsersProvider;


        public BasicTokenizerFactory(IInputReaderFactory<TInput, TParserOutput> inputReaderFactory, ITokenParsersProvider<TParserOutput> parsersProvider)
        {
            _inputReaderFactory = inputReaderFactory;
            _parsersProvider = parsersProvider;
        }

        public ITokenizer<TInput> Create()
        {
            return new BasicTokenizer<TInput, TParserOutput>(_inputReaderFactory, _parsersProvider.Provide());
        }
    }
}