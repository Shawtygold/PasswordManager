using PasswordBox.Interfaces.Repositories.Base;
using PasswordBox.MVVM.Model.Entities;

namespace PasswordBox.Interfaces.Repositories
{
    internal interface IUserRepository : 
        IRepositoryAdd<User>,
        IRepositoryGetAll<User>,
        IRepositoryDataChangedEvent<User>,
        IDisposable { }
}
