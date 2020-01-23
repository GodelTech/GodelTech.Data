using System.Collections.Generic;
using GodelTech.Data.Tests.Fakes;
using Xunit;

namespace GodelTech.Data.Tests
{
    public class PagedResultTests
    {
        private readonly PagedResult<FakeEntity> _pagedResult;

        public PagedResultTests()
        {
            _pagedResult = new PagedResult<FakeEntity>();
        }

        [Fact]
        public void PageIndex_Success()
        {
            // Arrange
            const int pageIndex = 1;

            // Act
            _pagedResult.PageIndex = pageIndex;

            // Assert
            Assert.Equal(pageIndex, _pagedResult.PageIndex);
        }

        [Fact]
        public void PageSize_Success()
        {
            // Arrange
            const int pageSize = 1;

            // Act
            _pagedResult.PageSize = pageSize;

            // Assert
            Assert.Equal(pageSize, _pagedResult.PageSize);
        }

        [Fact]
        public void Items_Success()
        {
            // Arrange
            var items = new List<FakeEntity>();

            // Act
            _pagedResult.Items = items;

            // Assert
            Assert.Equal(items, _pagedResult.Items);
        }

        [Fact]
        public void TotalCount_Success()
        {
            // Arrange
            const int totalCount = 1;

            // Act
            _pagedResult.TotalCount = totalCount;

            // Assert
            Assert.Equal(totalCount, _pagedResult.TotalCount);
        }
    }
}