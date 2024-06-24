using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordBox.MVVM.Model.Entities
{
    internal class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Login { get; set; }

        public User(string login) : this(0, login) { }
        public User(int id, string login)
        {
            Id = id;
            Login = login;
        }
    }
}
