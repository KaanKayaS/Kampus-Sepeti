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
    public class RevokeRules : BaseRules
    {
        public Task EmailAddressShouldBeValid(User? user)
        {
            if(user == null)
                throw new EmailAddressShouldBeValidException();

            return Task.CompletedTask;
        }
    }
}
