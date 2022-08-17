using System;
using System.Linq.Expressions;

namespace GodelTech.Data.Tests.Fakes
{
    public class FakeSpecification<TEntity, TKey> : Specification<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        private readonly Expression<Func<TEntity, bool>> _expression;

        public FakeSpecification(Expression<Func<TEntity, bool>> expression)
        {
            _expression = expression;
        }

        public override Expression<Func<TEntity, bool>> AsExpression()
        {
            return _expression;
        }
    }
}
