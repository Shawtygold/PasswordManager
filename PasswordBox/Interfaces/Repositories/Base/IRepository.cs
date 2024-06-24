using PasswordBox.MVVM.Model.EventArgs;
using System.Linq.Expressions;

namespace PasswordBox.Interfaces.Repositories.Base
{
    internal interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        Task AddAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task<TEntity?> GetByAsync(Expression<Func<TEntity, bool>> expression);
        event Action<DatabaseChangedEventArgs<TEntity>>? OnDataChanged;
    }
}
