using FluentValidation;
using PasswordBox.MVVM.Model.Entities;

namespace PasswordBox.MVVM.Model.Validators
{
    internal class UserValidator : AbstractValidator<User>
    {
        const int loginMinLenght = 2;
        const int loginMaxLenght = 50;

        public UserValidator()
        {
            RuleFor(u => u.Id).NotNull().GreaterThan(-1);
            RuleFor(u => u.Login).NotNull().MinimumLength(loginMinLenght).MaximumLength(loginMaxLenght);
        }
    }
}
