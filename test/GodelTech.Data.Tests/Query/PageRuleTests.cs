using Xunit;

namespace GodelTech.Data.Tests.Query
{
    public class PageRuleTests
    {
        public static TheoryData<PageRule, bool> IsValidTestData =>
            new TheoryData<PageRule, bool>
            {
                {
                    new PageRule(),
                    false
                },
                {
                    new PageRule
                    {
                        Index = -1
                    },
                    false
                },
                {
                    new PageRule
                    {
                        Size = -1
                    },
                    false
                },
                {
                    new PageRule
                    {
                        Index = -1,
                        Size = -1
                    },
                    false
                },
                {
                    new PageRule
                    {
                        Index = 1
                    },
                    false
                },
                {
                    new PageRule
                    {
                        Size = 1
                    },
                    true
                },
                {
                    new PageRule
                    {
                        Index = 1,
                        Size = 1
                    },
                    true
                }
            };

        [Theory]
        [MemberData(nameof(IsValidTestData))]
        public void IsValid_Success(
            PageRule pageRule,
            bool expectedResult)
        {
            // Arrange & Act & Assert
            Assert.Equal(expectedResult, pageRule.IsValid);
        }
    }
}
