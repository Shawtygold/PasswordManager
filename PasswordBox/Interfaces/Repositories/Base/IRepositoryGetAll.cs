namespace PasswordBox.Interfaces.Repositories.Base
{
    internal interface IRepositoryGetAll<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
    }
}
