using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordBox.MVVM.Model.Entities
{
    internal class User
    {
        public User(string login, string password) : this(0, login, password, "secretKey") { }
        public User(string login, string password, string secretKey) : this(0, login, password, secretKey) { }
        public User(int id, string login, string password, string secretKey)
        {
            Id = id;
            Login = login;
            SecretKey = secretKey;
        }


        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Login { get; set; }
        [Required, MinLength(4)]
        public string Password { get; set; }
        [Required]
        public string SecretKey { get; set; }
    }
}
