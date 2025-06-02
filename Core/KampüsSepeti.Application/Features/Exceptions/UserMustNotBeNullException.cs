using KampüsSepeti.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Exceptions
{   
    public class UserMustNotBeNullException : BaseException
    {
        public UserMustNotBeNullException() : base("Bu Id'ye sahip bir kullanıcı bulunamadı") { }

    }
    
}
