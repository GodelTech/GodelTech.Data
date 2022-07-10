using System;
using System.Linq.Expressions;
using GodelTech.Data.Tests.Fakes;
using Xunit;

namespace GodelTech.Data.Tests
{
    public class LinqSpecificationTests
    {
        [Fact]
        public void IsSatisfiedBy_Success()
        {
            // Arrange
            Expression<Func<FakeEntity<int>, bool>> expression = x => x.Id == 1;

            var specification = new FakeLinqSpecification<int>(expression);

            // Act
        }
    }
}
