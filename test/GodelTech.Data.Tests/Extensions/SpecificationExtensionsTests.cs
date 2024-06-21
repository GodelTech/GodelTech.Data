using System;
using GodelTech.Data.Tests.Fakes;
using Xunit;

namespace GodelTech.Data.Tests.Extensions
{
    public class SpecificationExtensionsTests
    {
        [Theory]
        [MemberData(nameof(TypesTestData.TypesGuidTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesIntTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesStringTestData), MemberType = typeof(TypesTestData))]
        public void CreateQueryParameters_WhenSpecificationIsNull_ThrowsArgumentNullException<TKey>(TKey id)
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => SpecificationExtensions.CreateQueryParameters<IEntity<TKey>, TKey>(null)
            );

            Assert.NotNull(id);
            Assert.Equal("specification", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(TypesTestData.TypesGuidTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesIntTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesStringTestData), MemberType = typeof(TypesTestData))]
        public void CreateQueryParameters_ReturnsQueryParameters<TKey>(TKey id)
        {
            // Arrange
            var filterExpression = FilterExpressionExtensionsTests.GetFilterExpression<IEntity<TKey>, TKey>(id);

            var specification = new FakeSpecification<IEntity<TKey>, TKey>(filterExpression);

            // Act
            var result = specification.CreateQueryParameters();

            // Assert
            Assert.Equal(result.Filter.Expression, filterExpression);
            Assert.Null(result.Sort);
            Assert.Null(result.Page);
        }
    }
}
