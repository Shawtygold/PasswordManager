using FluentValidation;
using PasswordBox.MVVM.Model.Validators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordBox.MVVM.Model.Entities
{
    internal class User
    {
        private readonly UserValidator _userValidator;

        public User(string login, string password, string secretKey) : this(0, login, password, secretKey) { }
        public User(int id, string login, string password, string secretKey)
        {
            Id = id;
            Login = login;
            Password = password;
            SecretKey = secretKey;

            _userValidator = new UserValidator();
            _userValidator.ValidateAndThrow(this);
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
