using Microsoft.EntityFrameworkCore;
using PasswordBox.Enums;
using PasswordBox.Interfaces.Repositories.Base;
using PasswordBox.MVVM.Model.AppContext;
using PasswordBox.MVVM.Model.Entities;
using PasswordBox.MVVM.Model.EventArgs;
using System.Linq.Expressions;

namespace PasswordBox.MVVM.Model.Repositories
{
    internal class PasswordRepository : IRepository<Password>, IDisposable
    {
        private readonly ApplicationContext _dbContext;

        public event Action<DatabaseChangedEventArgs<Password>>? OnDataChanged;

        public PasswordRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Password> GetAll()
        {
            return _dbContext.Passwords.AsNoTracking();
        }

        public async Task AddAsync(Password entity)
        {
            await _dbContext.Passwords.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            OnDataChanged?.Invoke(new DatabaseChangedEventArgs<Password>(ActionWithData.Add, entity));
        }

        public async Task DeleteAsync(Password entity)
        {
            await _dbContext.Passwords.Where(e => e.Id == entity.Id).ExecuteDeleteAsync();
            OnDataChanged?.Invoke(new DatabaseChangedEventArgs<Password>(ActionWithData.Delete, entity));
        }

        public async Task UpdateAsync(Password entity)
        {
            _dbContext.Passwords.Update(entity);
            await _dbContext.SaveChangesAsync();

            OnDataChanged?.Invoke(new DatabaseChangedEventArgs<Password>(ActionWithData.Update, entity));
        }

        public async Task<Password?> GetByAsync(Expression<Func<Password, bool>> expression)
        {
            return await _dbContext.Passwords.FirstOrDefaultAsync(expression);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
