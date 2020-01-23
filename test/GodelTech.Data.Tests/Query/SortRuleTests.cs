using System;
using System.Linq.Expressions;
using GodelTech.Data.Tests.Fakes;
using Xunit;

namespace GodelTech.Data.Tests.Query
{
    public class SortRuleTests
    {
        private readonly SortRule<FakeEntity, int> _sortRule;

        public SortRuleTests()
        {
            _sortRule = new SortRule<FakeEntity, int>();
        }

        [Fact]
        public void SortOrder_Success()
        {
            // Arrange
            const SortOrder sortOrder = SortOrder.Descending;

            // Act
            _sortRule.SortOrder = sortOrder;

            // Assert
            Assert.Equal(sortOrder, _sortRule.SortOrder);
        }

        [Fact]
        public void Expression_Success()
        {
            // Arrange
            Expression<Func<FakeEntity, object>> expression = x => x.Id == 1;

            // Act
            _sortRule.Expression = expression;

            // Assert
            Assert.Equal(expression, _sortRule.Expression);
        }
    }
}