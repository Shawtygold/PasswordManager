using PasswordBox.Interfaces.Repositories.Base;
using PasswordBox.MVVM.Model.Entities;

namespace PasswordBox.Interfaces.Repositories
{
    internal interface IPasswordRepository :
        IRepositoryGetAll<Password>, 
        IRepositoryAdd<Password>, 
        IRepositoryUpdate<Password>,
        IRepositoryDelete<Password>,
        IRepositoryDataChangedEvent<Password>,
        IRepositoryGetBy<Password>
    {
    }
}
