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
    public class LoginRules : BaseRules
    {
        public Task FailEmailOrPassword(User user, bool checkPassword)
        {
            if (user is  null || !checkPassword)
            {
                throw new FailEmailOrPasswordException();
            }
            return Task.CompletedTask;
        }
    }
}
