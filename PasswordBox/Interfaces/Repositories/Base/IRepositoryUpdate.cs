namespace PasswordBox.Interfaces.Repositories.Base
{
    internal interface IRepositoryUpdate<TEntity> where TEntity : class
    {
        Task UpdateAsync(TEntity entity);
    }
}
