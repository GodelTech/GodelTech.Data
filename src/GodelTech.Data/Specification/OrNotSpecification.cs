namespace GodelTech.Data.Specification
{
    internal class OrNotSpecification<TEntity, TKey> : CompositeSpecification<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        private readonly ISpecification<TEntity, TKey> _left;
        private readonly ISpecification<TEntity, TKey> _right;

        public OrNotSpecification(ISpecification<TEntity, TKey> left, ISpecification<TEntity, TKey> right)
        {
            _left = left;
            _right = right;
        }

        public override bool IsSatisfiedBy(TEntity candidate) => _left.IsSatisfiedBy(candidate) || !_right.IsSatisfiedBy(candidate);
    }
}
