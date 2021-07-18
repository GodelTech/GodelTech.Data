namespace GodelTech.Data
{
    /// <summary>
    /// Interface of repository for data layer.
    /// </summary>
    /// <typeparam name="TEntity">The type of the T entity.</typeparam>
    /// <typeparam name="TType">The type of the T type.</typeparam>
    public partial interface IRepository<TEntity, TType>
        where TEntity : class, IEntity<TType>
    {

    }
}