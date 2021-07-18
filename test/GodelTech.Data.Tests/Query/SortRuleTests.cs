using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xunit;

namespace GodelTech.Data.Tests.Query
{
    public class SortRuleTests
    {
        public static IEnumerable<object[]> SortOrderMemberData =>
            new Collection<object[]>
            {
                // Guid
                new object[]
                {
                    new SortRule<IEntity<Guid>, Guid>(),
                    SortOrder.Ascending
                },
                // int
                new object[]
                {
                    new SortRule<IEntity<int>, int>
                    {
                        SortOrder = SortOrder.Ascending
                    },
                    SortOrder.Ascending
                },
                // string
                new object[]
                {
                    new SortRule<IEntity<string>, string>
                    {
                        SortOrder = SortOrder.Descending
                    },
                    SortOrder.Descending
                }
            };

        [Theory]
        [MemberData(nameof(SortOrderMemberData))]
        public void SortOrder_Success<TKey>(
            SortRule<IEntity<TKey>, TKey> sortRule,
            SortOrder expectedSortOrder)
        {
            // Arrange & Act & Assert
            Assert.Equal(expectedSortOrder, sortRule?.SortOrder);
        }

        public static IEnumerable<object[]> IsValidMemberData =>
            new Collection<object[]>
            {
                // Guid
                new object[]
                {
                    new SortRule<IEntity<Guid>, Guid>(),
                    false
                },
                new object[]
                {
                    new SortRule<IEntity<Guid>, Guid>
                    {
                        Expression = null
                    },
                    false
                },
                new object[]
                {
                    new SortRule<IEntity<Guid>, Guid>
                    {
                        Expression = entity => entity.Id
                    },
                    true
                },
                // int
                new object[]
                {
                    new SortRule<IEntity<int>, int>(),
                    false
                },
                new object[]
                {
                    new SortRule<IEntity<int>, int>
                    {
                        Expression = null
                    },
                    false
                },
                new object[]
                {
                    new SortRule<IEntity<int>, int>
                    {
                        Expression = entity => entity.Id
                    },
                    true
                },
                // string
                new object[]
                {
                    new SortRule<IEntity<string>, string>(),
                    false
                },
                new object[]
                {
                    new SortRule<IEntity<string>, string>
                    {
                        Expression = null
                    },
                    false
                },
                new object[]
                {
                    new SortRule<IEntity<string>, string>
                    {
                        Expression = entity => entity.Id
                    },
                    true
                }
            };

        [Theory]
        [MemberData(nameof(IsValidMemberData))]
        public void IsValid_Success<TEntity, TKey>(
            SortRule<TEntity, TKey> sortRule,
            bool expectedResult)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange & Act & Assert
            Assert.Equal(expectedResult, sortRule?.IsValid);
        }
    }
}