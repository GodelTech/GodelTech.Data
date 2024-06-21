using System;
using System.Linq.Expressions;

namespace GodelTech.Data.Tests.Fakes
{
    public class SpecificationTestDataModel<TKey>
    {
        public FakeEntity<TKey> Entity { get; set; }

        public Expression<Func<FakeEntity<TKey>, bool>> LeftExpression { get; set; }

        public Expression<Func<FakeEntity<TKey>, bool>> RightExpression { get; set; }
    }
}
