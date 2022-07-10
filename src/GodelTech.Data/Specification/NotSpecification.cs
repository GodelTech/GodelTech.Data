namespace GodelTech.Data.Specification
{
    internal class NotSpecification<TEntity, TKey> : CompositeSpecification<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        private readonly ISpecification<TEntity, TKey> _other;

        public NotSpecification(ISpecification<TEntity, TKey> other)
        {
            _other = other;
        }

        public override bool IsSatisfiedBy(TEntity candidate) => !_other.IsSatisfiedBy(candidate);
    }
}
