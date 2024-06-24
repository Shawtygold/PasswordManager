using System.Linq.Expressions;

namespace PasswordBox.Interfaces.Repositories.Base
{
    internal interface IRepositoryGetBy<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByAsync(Expression<Func<TEntity, bool>> expression);
    }
}
