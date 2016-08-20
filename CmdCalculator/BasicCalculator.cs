using System.Collections.Generic;
using System.Linq;
using CmdCalculator.Exceptions;
using CmdCalculator.Interfaces;
using CmdCalculator.Interfaces.Evaluations;
using CmdCalculator.Interfaces.Parsers;
using CmdCalculator.Interfaces.Tokens;
using CmdCalculator.Parsers;

namespace CmdCalculator
{
    public class BasicCalculator<TInput, TOutput> : ICalculator<TInput, TOutput>
    {
        private readonly IExpressionParser _expressionParser;
        private readonly ITokenizer<TInput> _inputTokenizer;
        private readonly IEvaluationVisitor<TOutput> _visitor;

       
        public BasicCalculator(ITokenizer<TInput> inputTokenizer, IEvaluationVisitor<TOutput> resultEvaluator, IEnumerable<IExpressionParser> operatorParsers)
        {
            _inputTokenizer = inputTokenizer;
            _visitor = resultEvaluator;
            _expressionParser = new AllExpressionsParser(operatorParsers);
        }

        public TOutput Calculate(TInput input)
        {
            var tokenizedInput = _inputTokenizer.Tokenize(input);

            if (!tokenizedInput.Any())
            {
                return default(TOutput);
            }

            var topExpression = _expressionParser.ParseExpression(tokenizedInput, null);
            if (topExpression == null)
            {
                var message = string.Format("The expression \"{0}\" could not be parsed. Please try again.", input);
                throw new CalculatorException(message);
            }

            return _visitor.Visit(topExpression);
        }
    }
}
