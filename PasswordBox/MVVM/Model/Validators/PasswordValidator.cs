using FluentValidation;
using PasswordBox.MVVM.Model.Entities;

namespace PasswordBox.MVVM.Model.Validators
{
    internal class PasswordValidator : AbstractValidator<Password>
    {
        const int maxLoginLength = 50;
        const int maxPasswordLength = 80;

        public PasswordValidator()
        {
            RuleFor(p => p.Id).NotNull().GreaterThan(-1);
            RuleFor(p => p.Title).NotNull().MaximumLength(50);
            RuleFor(p => p.Login).NotNull().MaximumLength(maxLoginLength);
            RuleFor(p => p.PasswordHash).NotNull().MaximumLength(maxPasswordLength);
            RuleFor(p => p.Image).NotNull();
        }
    }
}
