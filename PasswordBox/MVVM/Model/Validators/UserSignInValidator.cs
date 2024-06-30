using FluentValidation;
using PasswordBox.MVVM.Model.AppContext;
using PasswordBox.MVVM.Model.Entities;
using PasswordBox.MVVM.Model.Repositories;

namespace PasswordBox.MVVM.Model.Validators
{
    internal class UserSignInValidator : AbstractValidator<User>
    {
        public UserSignInValidator()
        {
            RuleFor(u => u.Login).NotNull().CustomAsync(async (login, context, cancellationToken) =>
            {
                User? dbUser = null;
                using (UserRepository userRepository = new(new ApplicationContext()))
                {
                    try
                    {
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

                if (dbUser == null)
                {
                    context.AddFailure("User with this login does not exist.");
                    return;
                }

                if (context.InstanceToValidate.Password != dbUser.Password)
                {
                    context.AddFailure("Incorrect password.");
                    return;
                }
            });
        }
    }
}
