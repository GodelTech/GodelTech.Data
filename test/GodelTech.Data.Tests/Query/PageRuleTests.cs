using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xunit;

namespace GodelTech.Data.Tests.Query
{
    public class PageRuleTests
    {
        public static IEnumerable<object[]> IsValidMemberData =>
            new Collection<object[]>
            {
                new object[]
                {
                    new PageRule(),
                    false
                },
                new object[]
                {
                    new PageRule
                    {
                        Index = -1
                    },
                    false
                },
                new object[]
                {
                    new PageRule
                    {
                        Size = -1
                    },
                    false
                },
                new object[]
                {
                    new PageRule
                    {
                        Index = -1,
                        Size = -1
                    },
                    false
                },
                new object[]
                {
                    new PageRule
                    {
                        Index = 1
                    },
                    false
                },
                new object[]
                {
                    new PageRule
                    {
                        Size = 1
                    },
                    true
                },
                new object[]
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
        [MemberData(nameof(IsValidMemberData))]
        public void IsValid_Success(
            PageRule pageRule,
            bool expectedResult)
        {
            // Arrange & Act & Assert
            Assert.Equal(expectedResult, pageRule?.IsValid);
        }
    }
}
