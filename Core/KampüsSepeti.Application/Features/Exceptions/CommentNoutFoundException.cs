using KampüsSepeti.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Exceptions
{
    public class CommentNoutFoundException : BaseException
    {
        public CommentNoutFoundException() : base("Yorum bulunamadı.") { }
    }
}
