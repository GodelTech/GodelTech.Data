using System;
using System.Linq.Expressions;
using GodelTech.Data.Tests.Fakes;

namespace GodelTech.Data.Tests.TestData
{
    public class SpecificationTestDataModel<TKey>
    {
        public FakeEntity<TKey> Entity { get; set; }

        public Expression<Func<FakeEntity<TKey>, bool>> LeftExpression { get; set; }

        public Expression<Func<FakeEntity<TKey>, bool>> RightExpression { get; set; }
    }
}
