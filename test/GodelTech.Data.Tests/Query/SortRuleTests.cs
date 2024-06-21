using GodelTech.Data.Tests.Fakes;
using Xunit;

namespace GodelTech.Data.Tests.Query
{
    public class SortRuleTests
    {
        [Theory]
        [MemberData(nameof(TypesTestData.TypesGuidTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesIntTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesStringTestData), MemberType = typeof(TypesTestData))]
        public void SortOrder_Success<TKey>(TKey id)
        {
            // Arrange & Act & Assert
            Assert.NotNull(id);
            Assert.Equal(
                SortOrder.Ascending,
                new SortRule<IEntity<TKey>, TKey>().SortOrder
            );
            Assert.Equal(
                SortOrder.Ascending,
                new SortRule<IEntity<TKey>, TKey>
                {
                    SortOrder = SortOrder.Ascending
                }.SortOrder
            );
            Assert.Equal(
                SortOrder.Descending,
                new SortRule<IEntity<TKey>, TKey>
                {
                    SortOrder = SortOrder.Descending
                }.SortOrder
            );
        }

        [Theory]
        [MemberData(nameof(TypesTestData.TypesGuidTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesIntTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesStringTestData), MemberType = typeof(TypesTestData))]
        public void IsValid_Success<TKey>(TKey id)
        {
            // Arrange & Act & Assert
            Assert.NotNull(id);
            Assert.False(new SortRule<IEntity<TKey>, TKey>().IsValid);
            Assert.False(
                new SortRule<IEntity<TKey>, TKey>
                {
                    Expression = null
                }.IsValid
            );
            Assert.True(
                new SortRule<IEntity<TKey>, TKey>
                {
                    Expression = entity => entity.Id
                }.IsValid
            );
        }
    }
}
