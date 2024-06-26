﻿using System;
using GodelTech.Data.Tests.Fakes;
using GodelTech.Data.Tests.TestData;
using Moq;
using Xunit;

namespace GodelTech.Data.Tests.Query
{
    public class FilterRuleTests
    {
        [Theory]
        [MemberData(nameof(TypesTestData.TypesGuidTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesIntTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesStringTestData), MemberType = typeof(TypesTestData))]
        public void Constructor_WhenSpecificationIsNull_ThrowsArgumentNullException<TKey>(TKey id)
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => new FilterRule<FakeEntity<TKey>, TKey>(null)
            );

            Assert.NotNull(id);
            Assert.Equal("specification", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(TypesTestData.TypesGuidTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesIntTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesStringTestData), MemberType = typeof(TypesTestData))]
        public void Constructor<TKey>(TKey id)
        {
            // Arrange
            var expression = FilterExpressionExtensions.CreateIdFilterExpression<FakeEntity<TKey>, TKey>(id);

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
