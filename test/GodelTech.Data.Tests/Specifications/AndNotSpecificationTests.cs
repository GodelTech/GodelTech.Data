using System;
using System.Linq.Expressions;
using GodelTech.Data.Specifications;
using Xunit;

namespace GodelTech.Data.Tests.Specifications
{
    public class AndNotSpecificationTests
    {
        [Theory]
        [MemberData(nameof(CompositeSpecificationTests.MemberData), MemberType = typeof(CompositeSpecificationTests))]
        public void AsExpression_Success<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            Expression<Func<TEntity, bool>> leftExpression,
            Expression<Func<TEntity, bool>> rightExpression)
            where TEntity : class, IEntity<TKey>
        {
            if (leftExpression == null) throw new ArgumentNullException(nameof(leftExpression));
            if (rightExpression == null) throw new ArgumentNullException(nameof(rightExpression));

            CompositeSpecificationTests.AsExpression_Success(
                defaultKey,
                entity,
                leftExpression,
                rightExpression,
                (left, right) => new AndNotSpecification<TEntity, TKey>(left, right),
                leftExpression.Compile().Invoke(entity) && !rightExpression.Compile().Invoke(entity)
            );
        }
    }
}
