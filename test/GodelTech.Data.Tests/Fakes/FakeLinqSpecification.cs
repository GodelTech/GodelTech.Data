using System;
using System.Linq.Expressions;

namespace GodelTech.Data.Tests.Fakes
{
    public class FakeLinqSpecification<TKey> : LinqSpecification<FakeEntity<TKey>, TKey>
    {
        private readonly Expression<Func<FakeEntity<TKey>, bool>> _expression;

        public FakeLinqSpecification(Expression<Func<FakeEntity<TKey>, bool>> expression)
        {
            _expression = expression;
        }

        public override Expression<Func<FakeEntity<TKey>, bool>> AsExpression()
        {
            return _expression;
        }
    }
}
