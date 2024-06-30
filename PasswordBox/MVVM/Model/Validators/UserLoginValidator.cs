﻿using FluentValidation;
using PasswordBox.MVVM.Model.AppContext;
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
                if (string.IsNullOrEmpty(login))
                    return;

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
