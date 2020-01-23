using Xunit;

namespace GodelTech.Data.Tests.Query
{
    public class PageRuleTests
    {
        private readonly PageRule _pageRule;

        public PageRuleTests()
        {
            _pageRule = new PageRule();
        }

        [Fact]
        public void Index_Success()
        {
            // Arrange
            const int index = 1;

            // Act
            _pageRule.Index = index;

            // Assert
            Assert.Equal(index, _pageRule.Index);
        }

        [Fact]
        public void Size_Success()
        {
            // Arrange
            const int size = 1;

            // Act
            _pageRule.Size = size;

            // Assert
            Assert.Equal(size, _pageRule.Size);
        }

        [Theory]
        [InlineData(-1, 0, false)]
        [InlineData(-1, 1, false)]
        [InlineData(0, -1, false)]
        [InlineData(0, 0, false)]
        [InlineData(0, 1, true)]
        [InlineData(1, 1, true)]
        public void IsValid_Success(int index, int size, bool isValid)
        {
            // Arrange & Act
            _pageRule.Index = index;
            _pageRule.Size = size;

            // Assert
            Assert.Equal(isValid, _pageRule.IsValid);
        }
    }
}