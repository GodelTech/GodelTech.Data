using System.Linq.Expressions;
using GodelTech.Data.Tests.Fakes.Specifications;
using GodelTech.Data.Tests.TestData;
using Moq;
using Xunit;

namespace GodelTech.Data.Tests.Specifications
{
    public class CompositeSpecificationTests
    {
        [Theory]
        [MemberData(nameof(TypesTestData.TypesGuidTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesIntTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesStringTestData), MemberType = typeof(TypesTestData))]
        public void AsExpression_Success<TKey>(TKey id)
        {
            // Arrange
            var leftSpecification = new Mock<Specification<IEntity<TKey>, TKey>>(MockBehavior.Strict);

            var rightSpecification = new Mock<Specification<IEntity<TKey>, TKey>>(MockBehavior.Strict);

            // Act
            var result = new FakeNullCompositeSpecification<BinaryExpression, IEntity<TKey>, TKey>(
                leftSpecification.Object,
                rightSpecification.Object
            ).AsExpression();

            // Assert
            Assert.NotNull(id);
            Assert.Null(result);
        }
    }
}
