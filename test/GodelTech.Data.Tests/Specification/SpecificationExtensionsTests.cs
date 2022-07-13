using System;
using System.Linq.Expressions;
using GodelTech.Data.Specification;
using GodelTech.Data.Tests.Extensions;
using GodelTech.Data.Tests.Fakes;
using Xunit;

namespace GodelTech.Data.Tests.Specification
{
    public class SpecificationExtensionsTests
    {
        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void CreateQueryParameters_WhenSpecificationIsNull_ThrowsArgumentNullException<TKey>(TKey defaultKey)
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => SpecificationExtensions.CreateQueryParameters<IEntity<TKey>, TKey>(null)
            );

            Assert.NotNull(defaultKey);
            Assert.Equal("specification", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(SpecificationBaseTests.IsSatisfiedByMemberData), MemberType = typeof(SpecificationBaseTests))]
        public void CreateQueryParameters_ReturnsQueryParameters<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            Expression<Func<TEntity, bool>> expression,
            bool expectedResult)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var specification = new FakeSpecification<TEntity, TKey>(expression);

            // Act
            var result = specification.CreateQueryParameters();

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultKey.GetType(), entity.Id);
            }

            Assert.Equal(expectedResult, result.Filter.Expression.Compile().Invoke(entity));
            Assert.Null(result.Sort);
            Assert.Null(result.Page);
        }
    }
}
