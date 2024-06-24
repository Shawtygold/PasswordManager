using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Input;

namespace PasswordBox.MVVM.Model.Entities;

public partial class Password
{
    public Password(string title, string login, string passwordHash, string commentary, string image) : this(0, title, login, passwordHash, commentary, image)
    { }

    public Password(int id, string title, string login, string passwordHash, string commentary, string image)
    {
        Id = id;
        Title = title;
        Login = login;
        PasswordHash = passwordHash;
        Commentary = commentary;
        Image = image;
    }

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required, MaxLength(50)]
    public string Title { get; set; }
    [Required]
    public string Image { get; set; }
    [Required, MaxLength(50)]
    public string Login { get; set; } = null!;
    [Required, MaxLength(80)]
    public string PasswordHash { get; set; } = null!;
    public string Commentary { get; set; } = null!;
}
