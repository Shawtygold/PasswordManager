using FluentValidation;
using PasswordBox.MVVM.Model.Entities;
using PasswordBox.MVVM.Model.Repositories;

namespace PasswordBox.MVVM.Model.Validators
{
    internal class UserLoginValidator : UserValidator
    {
        public UserLoginValidator() : base()
        {
            RuleFor(u => u.Login).CustomAsync(async (login, context, cancellationToken) =>
            {
                User? user = null;
                using (UserRepository userRepository = new(new AppContext.ApplicationContext()))
                {
                    try
                    {
                        user = await userRepository.GetByAsync(u => u.Login == login);
                    }
                    catch (Exception e)
                    {
                        dynamic ex = e;
                        ExceptionsLogger.Handle(ex);
                        context.AddFailure("An error occurred during user verification.");
                        return;
                    }
                }

                if (user == null)
                {
                    context.AddFailure("User with this login does not exist.");
                    return;
                }
            });
        }
    }
}
