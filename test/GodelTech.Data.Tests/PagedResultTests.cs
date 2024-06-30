using System;
using System.Collections.ObjectModel;
using FluentAssertions;
using GodelTech.Data.Tests.Fakes;
using Xunit;

namespace GodelTech.Data.Tests
{
    public class PagedResultTests
    {
        public static TheoryData<PagedResult<FakeModel>, int, int, Collection<FakeModel>, int> ConstructorTestData =>
            new TheoryData<PagedResult<FakeModel>, int, int, Collection<FakeModel>, int>
            {
                {
                    new PagedResult<FakeModel>(
                        0,
                        0,
                        null,
                        0
                    ),
                    0,
                    0,
                    new Collection<FakeModel>(),
                    0
                },
                {
                    new PagedResult<FakeModel>(
                        1,
                        2,
                        new Collection<FakeModel>
                        {
                            new FakeModel
                            {
                                Id = 99
                            }
                        },
                        3
                    ),
                    1,
                    2,
                    new Collection<FakeModel>
                    {
                        new FakeModel
                        {
                            Id = 99
                        }
                    },
                    3
                },
                {
                    new PagedResult<FakeModel>(
                        new PageRule(),
                        null,
                        0
                    ),
                    0,
                    0,
                    new Collection<FakeModel>(),
                    0
                },
                {
                    new PagedResult<FakeModel>(
                        new PageRule
                        {
                            Index = 1,
                            Size = 2
                        },
                        new Collection<FakeModel>
                        {
                            new FakeModel
                            {
                                Id = 99
                            }
                        },
                        3
                    ),
                    1,
                    2,
                    new Collection<FakeModel>
                    {
                        new FakeModel
                        {
                            Id = 99
                        }
                    },
                    3
                }
            };

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void Constructor(
            PagedResult<FakeModel> item,
            int expectedPageIndex,
            int expectedPageSize,
            Collection<FakeModel> expectedItems,
            int expectedTotalCount)
        {
            // Arrange & Act & Assert
            Assert.Equal(expectedPageIndex, item.PageIndex);
            Assert.Equal(expectedPageSize, item.PageSize);
            item.Items.Should().BeEquivalentTo(expectedItems);
            Assert.Equal(expectedTotalCount, item.TotalCount);
        }

        [Fact]
        public void Constructor_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(
                () => new PagedResult<FakeModel>(
                    null,
                    new Collection<FakeModel>(),
                    0
                )
            );
        }
    }
}
