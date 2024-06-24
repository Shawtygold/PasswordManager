using FluentValidation;
using PasswordBox.MVVM.Model.Entities;

namespace PasswordBox.MVVM.Model.Validators
{
    internal class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Id).NotNull().GreaterThan(-1);
            RuleFor(u => u.Login).NotNull().MinimumLength(2).MaximumLength(50);
        }
    }
}
