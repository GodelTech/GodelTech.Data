using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GodelTech.Data.Tests.Fakes;
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
                    new SortRule<FakeEntity<Guid>, Guid>(),
                    SortOrder.Ascending
                },
                new object[]
                {
                    new SortRule<FakeEntity<int>, int>
                    {
                        SortOrder = SortOrder.Ascending
                    },
                    SortOrder.Ascending
                },
                new object[]
                {
                    new SortRule<FakeEntity<string>, string>
                    {
                        SortOrder = SortOrder.Descending
                    },
                    SortOrder.Descending
                }
            };

        [Theory]
        [MemberData(nameof(SortOrderMemberData))]
        public void SortOrder_Success<TType>(
            SortRule<FakeEntity<TType>, TType> sortRule,
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
                    new SortRule<FakeEntity<Guid>, Guid>(),
                    false
                },
                new object[]
                {
                    new SortRule<FakeEntity<Guid>, Guid>
                    {
                        Expression = null
                    },
                    false
                },
                new object[]
                {
                    new SortRule<FakeEntity<Guid>, Guid>
                    {
                        Expression = entity => entity.Id
                    },
                    true
                },
                new object[]
                {
                    new SortRule<FakeEntity<int>, int>(),
                    false
                },
                new object[]
                {
                    new SortRule<FakeEntity<int>, int>
                    {
                        Expression = null
                    },
                    false
                },
                new object[]
                {
                    new SortRule<FakeEntity<int>, int>
                    {
                        Expression = entity => entity.Id
                    },
                    true
                },
                new object[]
                {
                    new SortRule<FakeEntity<string>, string>(),
                    false
                },
                new object[]
                {
                    new SortRule<FakeEntity<string>, string>
                    {
                        Expression = null
                    },
                    false
                },
                new object[]
                {
                    new SortRule<FakeEntity<string>, string>
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