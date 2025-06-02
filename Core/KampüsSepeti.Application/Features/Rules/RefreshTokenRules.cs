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
    public class RefreshTokenRules : BaseRules
    {
        public Task RefreshTokenShouldNotBeExpired(DateTime? expiryDate)
        {
            if(expiryDate <= DateTime.UtcNow)
            {
                throw new RefreshTokenShouldNotBeExpiredException();
            }
            return Task.CompletedTask;
        }
    }
}
