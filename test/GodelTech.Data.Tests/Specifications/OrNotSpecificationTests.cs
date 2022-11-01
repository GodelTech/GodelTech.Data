using System;
using GodelTech.Data.Specifications;

namespace GodelTech.Data.Tests.Specifications
{
    public class OrNotSpecificationTests : SpecificationBaseTests
    {
        protected override Func<bool, bool, bool> Func => (left, right) => left || !right;

        protected override Specification<TEntity, TKey> CreateSpecification<TEntity, TKey>(
            Specification<TEntity, TKey> left,
            Specification<TEntity, TKey> right)
        {
            return new OrNotSpecification<TEntity, TKey>(left, right);
        }
    }
}
