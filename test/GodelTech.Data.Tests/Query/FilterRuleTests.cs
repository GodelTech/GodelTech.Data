using GodelTech.Data.Tests.Fakes;
using Moq;
using Neleus.LambdaCompare;
using Xunit;

namespace GodelTech.Data.Tests.Query
{
    public class FilterRuleTests
    {
        [Fact]
        public void Constructor()
        {
            // Arrange
            var entity = new FakeEntity<int>();

            var mockSpecification = new Mock<ISpecification<FakeEntity<int>, int>>(MockBehavior.Strict);
            mockSpecification
                .Setup(x => x.AsExpression())
                .Returns(x => x.Id > 0);

            // Act
            var filterRule = new FilterRule<FakeEntity<int>, int>(mockSpecification.Object);

            Assert.True(
                Lambda.Eq(
                    x => x.Id > 0,
                    filterRule.Expression
                )
            );
        }
    }
}
