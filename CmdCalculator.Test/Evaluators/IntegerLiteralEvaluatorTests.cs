using CmdCalculator.Evaluations;
using CmdCalculator.Expressions;
using CmdCalculator.Interfaces.Evaluations;
using NSubstitute;
using NUnit.Framework;

namespace CmdCalculator.Test.Evaluators
{
    [TestFixture]
    public class IntegerLiteralEvaluatorTests
    {
        [Test]
        public void Evaluator_Evaluates_Literal_Expression_Correctly()
        {
            //Arrange
            const int expectedResult = 6;
            var evaluator = new IntegerLiteralExpressionEvaluator();
            var expression = new LiteralExpression("6");
            var visitor = Substitute.For<IEvaluationVisitor<int>>();

            //Act
            var result = evaluator.Evaluate(expression, visitor);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
