namespace PasswordBox.Interfaces.Repositories.Base
{
    internal interface IRepositoryAdd<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
    }
}
