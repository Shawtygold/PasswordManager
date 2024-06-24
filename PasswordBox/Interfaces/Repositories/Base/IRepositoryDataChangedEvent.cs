using PasswordBox.MVVM.Model.EventArgs;

namespace PasswordBox.Interfaces.Repositories.Base
{
    internal interface IRepositoryDataChangedEvent<TEntity> where TEntity : class
    {
        event Action<DatabaseChangedEventArgs<TEntity>> OnDataChanged;
    }
}
