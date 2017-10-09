using CmdCalculator.Interfaces.Operators;
using CmdCalculator.Interfaces.Tokens;

namespace CmdCalculator.Tokenization.Tokens
{
    public class BinaryMathOpToken<TOp> : IOperatorToken<TOp>
        where TOp : IOperator, new()
    {
        public TOp Op{ get; private set; }

        public string OpRepresentation
        {
            get { return Op.OpRepresentation; }
        }

        public BinaryMathOpToken()
        {
            Op = new TOp();
        }

        public override bool Equals(object obj)
        {
            var token = obj as IOperatorToken<TOp>;
            if (token != null)
            {
                return token.Op.OpRepresentation == Op.OpRepresentation;
            }
            return base.Equals(obj);
        }
    }
}
