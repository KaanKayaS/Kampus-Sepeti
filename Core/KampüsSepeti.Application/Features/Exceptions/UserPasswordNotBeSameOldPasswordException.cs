using KampüsSepeti.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Exceptions
{
    public class UserPasswordNotBeSameOldPasswordException : BaseException
    {
        public UserPasswordNotBeSameOldPasswordException(): base("Yeni şifreniz eski şifre ile aynı olmamalıdır.") { }
    }
}
