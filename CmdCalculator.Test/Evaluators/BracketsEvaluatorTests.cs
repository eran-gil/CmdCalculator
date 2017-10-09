using CmdCalculator.Evaluations;
using CmdCalculator.Expressions;
using CmdCalculator.Interfaces.Evaluations;
using CmdCalculator.Interfaces.Expressions;
using NSubstitute;
using NUnit.Framework;

namespace CmdCalculator.Test.Evaluators
{
    [TestFixture]
    public class BracketsEvaluatorTests
    {
        private IExpression _dummyExpression;
        private BracketsEvaluator<int> _evaluator;

        [OneTimeSetUp]
        public void SetUpTest()
        {
            _dummyExpression = Substitute.For<IExpression>();
            _evaluator = new BracketsEvaluator<int>();
        }

        [Test]
        public void Evalutaor_Returns_Result_Of_Inner_Expression()
        {
            //Arrange
            const int expectedResult = 6;
            var visitor = Substitute.For<IEvaluationVisitor<int>>();//A.Fake<IEvaluationVisitor<int>>());
            visitor.Visit(_dummyExpression).Returns(expectedResult);
            var expression = new BracketOpExpression(_dummyExpression, 1);

            //Act
            var result = _evaluator.Evaluate(expression, visitor);

            //Assert
            visitor.Received(1).Visit(_dummyExpression);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Evaluator_Supports_Brackets_Op_Expression()
        {
            //Arrange
            var expectedType = typeof(BracketOpExpression);

            //Act
            var supportedType = _evaluator.GetSupportedExpressionType();

            //Assert
            Assert.AreEqual(expectedType, supportedType);
        }


    }
}
