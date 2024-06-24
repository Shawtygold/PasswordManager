namespace PasswordBox.Interfaces.Repositories.Base
{
    internal interface IRepositoryDelete<TEntity> where TEntity : class
    {
        Task DeleteAsync(TEntity entity);
    }
}
