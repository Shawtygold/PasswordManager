using FluentValidation;
using PasswordBox.Interfaces.Repositories;
using PasswordBox.MVVM.Model.AppContext;
using PasswordBox.MVVM.Model.Entities;
using PasswordBox.MVVM.Model.Repositories;

namespace PasswordBox.MVVM.Model.Validators
{
    internal class UserSignUpValidator : AbstractValidator<User>
    {
        public UserSignUpValidator()
        {
            RuleFor(u => u.Login).NotNull().CustomAsync(async (login, context, cancellation) =>
            {
                User? dbUser = null;
                using (IUserRepository userRepository = new UserRepository(new ApplicationContext()))
                {
                    try
                    {   // Получение пользователя из бд по логину
                        dbUser = await userRepository.GetByAsync(u => u.Login == login);
                    }
                    catch (Exception e)
                    {
                        dynamic ex = e;
                        ExceptionsLogger.Handle(ex);
                        context.AddFailure("An error occurred during user verification. Please contact with support.");
                        return;
                    }
                }

                if (dbUser != null)
                    context.AddFailure("User with this login already exists.");
            });
        }
    }
}
