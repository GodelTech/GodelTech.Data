namespace GodelTech.Data.Specification
{
    internal class AndNotSpecification<TEntity, TKey> : CompositeSpecification<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        private readonly ISpecification<TEntity, TKey> _left;
        private readonly ISpecification<TEntity, TKey> _right;

        public AndNotSpecification(ISpecification<TEntity, TKey> left, ISpecification<TEntity, TKey> right)
        {
            _left = left;
            _right = right;
        }

        public override bool IsSatisfiedBy(TEntity candidate) => _left.IsSatisfiedBy(candidate) && !_right.IsSatisfiedBy(candidate);
    }
}
