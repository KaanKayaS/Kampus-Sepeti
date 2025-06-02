using KampüsSepeti.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Exceptions
{
    public class UserFollowerNotFoundException : BaseException
    {
        public UserFollowerNotFoundException() : base("Bu kullanıcıyı zaten takip etmiyosunuz.") { }
    }
}
