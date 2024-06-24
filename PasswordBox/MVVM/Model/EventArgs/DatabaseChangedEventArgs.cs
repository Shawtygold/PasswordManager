using PasswordBox.Enums;

namespace PasswordBox.MVVM.Model.EventArgs
{
    internal class DatabaseChangedEventArgs<TEntity> where TEntity : class
    {
        public ActionWithData Action { get; set; }
        public TEntity DatabaseObject { get; set; } // Содержит объект, который был обновлен/добавлен или удален из базы данных
        public DatabaseChangedEventArgs(ActionWithData action, TEntity databaseObject)
        {
            Action = action;
            DatabaseObject = databaseObject;
        }
    }
}
