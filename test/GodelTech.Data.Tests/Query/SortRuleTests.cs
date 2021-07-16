using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GodelTech.Data.Tests.Fakes.Entities;
using Xunit;

namespace GodelTech.Data.Tests.Query
{
    public class SortRuleTests
    {
        public static IEnumerable<object[]> SortOrderMemberData =>
            new Collection<object[]>
            {
                new object[]
                {
                    new SortRule<FakeGuidEntity, Guid>(),
                    SortOrder.Ascending
                },
                new object[]
                {
                    new SortRule<FakeGuidEntity, Guid>
                    {
                        SortOrder = SortOrder.Ascending
                    },
                    SortOrder.Ascending
                },
                new object[]
                {
                    new SortRule<FakeGuidEntity, Guid>
                    {
                        SortOrder = SortOrder.Descending
                    },
                    SortOrder.Descending
                }
            };

        [Theory]
        [MemberData(nameof(SortOrderMemberData))]
        public void SortOrder_Success(
            SortRule<FakeGuidEntity, Guid> sortRule,
            SortOrder expectedSortOrder)
        {
            // Arrange & Act & Assert
            Assert.Equal(expectedSortOrder, sortRule?.SortOrder);
        }

        public static IEnumerable<object[]> IsValidMemberData =>
            new Collection<object[]>
            {
                new object[]
                {
                    new SortRule<FakeGuidEntity, Guid>(),
                    false
                },
                new object[]
                {
                    new SortRule<FakeGuidEntity, Guid>
                    {
                        Expression = null
                    },
                    false
                },
                new object[]
                {
                    new SortRule<FakeGuidEntity, Guid>
                    {
                        Expression = entity => entity.Id
                    },
                    true
                },
                new object[]
                {
                    new SortRule<FakeIntEntity, int>(),
                    false
                },
                new object[]
                {
                    new SortRule<FakeIntEntity, int>
                    {
                        Expression = null
                    },
                    false
                },
                new object[]
                {
                    new SortRule<FakeIntEntity, int>
                    {
                        Expression = entity => entity.Id
                    },
                    true
                },
                new object[]
                {
                    new SortRule<FakeStringEntity, string>(),
                    false
                },
                new object[]
                {
                    new SortRule<FakeStringEntity, string>
                    {
                        Expression = null
                    },
                    false
                },
                new object[]
                {
                    new SortRule<FakeStringEntity, string>
                    {
                        Expression = entity => entity.Id
                    },
                    true
                }
            };

        [Theory]
        [MemberData(nameof(IsValidMemberData))]
        public void IsValid_Success<TEntity, TType>(
            SortRule<TEntity, TType> sortRule,
            bool expectedResult)
            where TEntity : class, IEntity<TType>
        {
            // Arrange & Act & Assert
            Assert.Equal(expectedResult, sortRule?.IsValid);
        }
    }
}