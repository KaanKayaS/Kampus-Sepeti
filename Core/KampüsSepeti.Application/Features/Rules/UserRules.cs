using KampüsSepeti.Application.Bases;
using KampüsSepeti.Application.Features.Exceptions;
using KampüsSepeti.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Rules
{
    public class UserRules : BaseRules
    {
        public Task UserMustNotBeNull(User? user)
        {
            if (user == null) throw new UserMustNotBeNullException();
            return Task.CompletedTask;
        }

        public Task UserProfilePhotoAlreadyNull(User? user)
        {
            if (user.Image == null) throw new UserProfilePhotoAlreadyNullException();
            return Task.CompletedTask;
        }

        public Task UserOldPasswordWrong(bool isPasswordValid)
        {
            if (!isPasswordValid) throw new UserOldPasswordWrongException();
            return Task.CompletedTask;
        }
        public Task UserNewPasswordsDoesNotMatch(string password , string confirmPassword)
        {
            if (password != confirmPassword) throw new UserNewPasswordsDoesNotMatchException();
            return Task.CompletedTask;
        }

        public Task UserPasswordNotBeSameOldPassword(string password, string oldPassword)
        {
            if (password == oldPassword) throw new UserPasswordNotBeSameOldPasswordException();
            return Task.CompletedTask;
        }

    }
}
