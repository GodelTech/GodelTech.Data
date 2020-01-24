using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace GodelTech.Data.Tests.Query
{
    public class SortOrderTests
    {
        [Fact]
        public void IsEnum()
        {
            // Arrange & Act & Assert
            Assert.True(typeof(SortOrder).GetTypeInfo().IsEnum);
        }

        [Fact]
        public void Validate_Values()
        {
            // Arrange & Act & Assert
            Assert.Equal(
                new []
                {
                    SortOrder.Ascending,
                    SortOrder.Descending
                },
                Enum.GetValues(typeof(SortOrder)).Cast<SortOrder>()
            );
        }
    }
}