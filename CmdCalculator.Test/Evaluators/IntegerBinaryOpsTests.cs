using CmdCalculator.Evaluations;
using CmdCalculator.Expressions;
using CmdCalculator.Interfaces.Evaluations;
using CmdCalculator.Interfaces.Expressions;
using CmdCalculator.Interfaces.Operators;
using CmdCalculator.Operators;
using NSubstitute;
using NUnit.Framework;

namespace CmdCalculator.Test.Evaluators
{
    public class IntegerBinaryOpsTests
    {
        private IExpression _firstOperandDummy;
        private IExpression _secondOperandDummy;

        private static readonly IntegerAdditionEvaluator AdditionEvaluator = new IntegerAdditionEvaluator();
        private static readonly IntegerSubstractionEvaluator SubstractionEvaluator = new IntegerSubstractionEvaluator();
        private static readonly IntegerMultiplicationEvaluator MultiplicationEvaluator = new IntegerMultiplicationEvaluator();
        private static readonly IntegerDivisionEvaluator DivisionEvaluator = new IntegerDivisionEvaluator();

        private static readonly AdditionOperator AdditionOperator = new AdditionOperator();
        private static readonly SubtractionOperator SubtractionOperator = new SubtractionOperator();
        private static readonly MultiplicationOperator MultiplicationOperator = new MultiplicationOperator();
        private static readonly DivisionOperator DivisionOperator = new DivisionOperator();

        [OneTimeSetUp]
        public void SetUpTests()
        {
            _firstOperandDummy = Substitute.For<IExpression>();
            _secondOperandDummy = Substitute.For<IExpression>();
        }

        [Test, TestCaseSource(nameof(EvaluatorInputCases))]
        public void Evaluator_Calculates_Operands_Result_Correctly<TOp>(
            TOp operatorType,
            BinaryExpressionEvaluatorBase<TOp, int> evaluator,
            int firstOperand, int secondOperand, int expected)
            where TOp : IOperator
        {
            //Arrange
            var visitor = Substitute.For<IEvaluationVisitor<int>>();
            visitor.Visit(_firstOperandDummy).Returns(firstOperand);
            visitor.Visit(_secondOperandDummy).Returns(secondOperand);
            var expression = new BinaryOpExpression<TOp>(_firstOperandDummy, _secondOperandDummy, 1);

            //Act
            var result = evaluator.Evaluate(expression, visitor);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test, TestCaseSource(nameof(EvaluatorTypeCases))]
        public void Evaluator_Supports_Correct_Expression_Type<TOp>(
            TOp operatorType,
            BinaryExpressionEvaluatorBase<TOp, int> evaluator)
            where TOp : IOperator
        {
            //Arrange
            var expectedType = typeof(BinaryOpExpression<TOp>);

            //Act
            var supportedType = evaluator.GetSupportedExpressionType();

            //Assert
            Assert.AreEqual(expectedType, supportedType);
        }

        private static readonly TestCaseData[] EvaluatorInputCases =
        {
            new TestCaseData(AdditionOperator, AdditionEvaluator, 6, 2, 8).SetName("6+2=8"),
            new TestCaseData(SubtractionOperator, SubstractionEvaluator, 6, 2, 4).SetName("6-2=4"),
            new TestCaseData(MultiplicationOperator, MultiplicationEvaluator, 6, 2, 12).SetName("6*2=12"),
            new TestCaseData(DivisionOperator, DivisionEvaluator, 6, 2, 3).SetName("6/2=3"),
        };

        private static readonly TestCaseData[] EvaluatorTypeCases =
        {
            new TestCaseData(AdditionOperator, AdditionEvaluator).SetName("Addition operator"),
            new TestCaseData(SubtractionOperator, SubstractionEvaluator).SetName("Subtraction operator"),
            new TestCaseData(MultiplicationOperator, MultiplicationEvaluator).SetName("Multiplication operator"),
            new TestCaseData(DivisionOperator, DivisionEvaluator).SetName("Division operator"),
        };
    }
}
