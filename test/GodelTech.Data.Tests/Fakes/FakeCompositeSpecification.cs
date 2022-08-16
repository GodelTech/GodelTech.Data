using System;
using System.Linq.Expressions;

using GodelTech.Data.Specification;

namespace GodelTech.Data.Tests.Fakes
{
    public class FakeCompositeSpecification<TEntity, TKey> : CompositeSpecification<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        private readonly bool _isSatisfiedBy;

        public FakeCompositeSpecification(bool isSatisfiedBy)
        {
            _isSatisfiedBy = isSatisfiedBy;
        }

        public override Expression<Func<TEntity, bool>> AsExpression() => (x) => _isSatisfiedBy;
    }
}
