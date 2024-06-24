namespace PasswordBox.MVVM.Model.Entities
{
    internal class User
    {
        public int Id { get; set; }
        public string Login { get; set; }

        public User(string login) : this(0, login) { }
        public User(int id, string login)
        {
            Id = id;
            Login = login;
        }
    }
}
