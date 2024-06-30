using FluentValidation;
using PasswordBox.MVVM.Model.Entities;

namespace PasswordBox.MVVM.Model.Validators
{
    internal class UserValidator : AbstractValidator<User>
    {
        const int loginMinLenght = 2;
        const int loginMaxLenght = 50;
        const int passwordMinLenght = 4;

        public UserValidator()
        {
            RuleFor(u => u.Id).NotNull().GreaterThan(-1);
            RuleFor(u => u.Login).NotNull().MinimumLength(loginMinLenght).MaximumLength(loginMaxLenght);
            RuleFor(u => u.Password).NotNull().MinimumLength(passwordMinLenght);
            RuleFor(u => u.SecretKey).NotNull();
        }
    }
}
