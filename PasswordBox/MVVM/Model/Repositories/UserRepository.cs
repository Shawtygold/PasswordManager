using Microsoft.EntityFrameworkCore;
using PasswordBox.Enums;
using PasswordBox.Interfaces.Repositories;
using PasswordBox.MVVM.Model.AppContext;
using PasswordBox.MVVM.Model.Entities;
using PasswordBox.MVVM.Model.EventArgs;
using System.Linq.Expressions;

namespace PasswordBox.MVVM.Model.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _dbContext;
        public event Action<DatabaseChangedEventArgs<User>>? OnDataChanged;

        public UserRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<User> GetAll()
        {
            return _dbContext.Users.AsNoTracking();
        }

        public async Task AddAsync(User entity)
        {
            await _dbContext.Users.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            OnDataChanged?.Invoke(new(ActionWithData.Add, entity));
        }

        public async Task<User?> GetByAsync(Expression<Func<User, bool>> expression)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(expression);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
