using System;
using GodelTech.Data.Specifications;

namespace GodelTech.Data.Tests.Specifications
{
    public class NotSpecificationTests : SpecificationBaseTests
    {
        protected override Func<bool, bool, bool> Func => (left, _) => !left;

        protected override Specification<TEntity, TKey> CreateSpecification<TEntity, TKey>(
            Specification<TEntity, TKey> left,
            Specification<TEntity, TKey> right)
        {
            return new NotSpecification<TEntity, TKey>(left);
        }
    }
}
