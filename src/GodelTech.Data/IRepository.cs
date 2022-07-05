namespace GodelTech.Data
{
    /// <summary>
    /// Interface of repository for data layer.
    /// </summary>
    /// <typeparam name="TEntity">The type of the T entity.</typeparam>
    /// <typeparam name="TKey">The type of the T key.</typeparam>
    public partial interface IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {

    }
}
