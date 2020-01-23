using GodelTech.Data.Tests.Fakes;
using Xunit;

namespace GodelTech.Data.Tests
{
    public class QueryParametersTests
    {
        private readonly QueryParameters<FakeEntity, int> _queryParameters;

        public QueryParametersTests()
        {
            _queryParameters = new QueryParameters<FakeEntity, int>();
        }

        [Fact]
        public void Filter_Success()
        {
            // Arrange
            var filter = new FilterRule<FakeEntity, int>();

            // Act
            _queryParameters.Filter = filter;

            // Assert
            Assert.Equal(filter, _queryParameters.Filter);
        }

        [Fact]
        public void Sort_Success()
        {
            // Arrange
            var sort = new SortRule<FakeEntity, int>();

            // Act
            _queryParameters.Sort = sort;

            // Assert
            Assert.Equal(sort, _queryParameters.Sort);
        }

        [Fact]
        public void Page_Success()
        {
            // Arrange
            var page = new PageRule();

            // Act
            _queryParameters.Page = page;

            // Assert
            Assert.Equal(page, _queryParameters.Page);
        }
    }
}