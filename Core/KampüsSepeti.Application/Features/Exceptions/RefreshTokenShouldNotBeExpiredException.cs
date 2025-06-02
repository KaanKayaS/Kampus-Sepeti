using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Exceptions
{
    public class RefreshTokenShouldNotBeExpiredException : NotFoundException
    {
        public RefreshTokenShouldNotBeExpiredException() : base("Oturum süresi sona ermiştir. Lütfen tekrar giriş yapınız.") { }

    }
    
}
