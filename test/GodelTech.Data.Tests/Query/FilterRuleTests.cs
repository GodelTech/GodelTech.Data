using System;
using GodelTech.Data.Tests.Extensions;
using GodelTech.Data.Tests.Fakes;
using Moq;
using Xunit;

namespace GodelTech.Data.Tests.Query
{
    public class FilterRuleTests
    {
        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void Constructor_WhenSpecificationIsNull_ThrowsArgumentNullException<TKey>(TKey defaultKey)
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => new FilterRule<FakeEntity<TKey>, TKey>(null)
            );

            Assert.NotNull(defaultKey);
            Assert.Equal("specification", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void Constructor<TKey>(TKey defaultKey)
        {
            // Arrange
            var expression = FilterExpressionExtensions.CreateIdFilterExpression<FakeEntity<TKey>, TKey>(defaultKey);

            var mockSpecification = new Mock<Specification<FakeEntity<TKey>, TKey>>(MockBehavior.Strict);
            mockSpecification
                .Setup(x => x.AsExpression())
                .Returns(expression);

            // Act
            var filterRule = new FilterRule<FakeEntity<TKey>, TKey>(mockSpecification.Object);

            Assert.Equal(expression, filterRule.Expression);
        }
    }
}
