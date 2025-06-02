using KampüsSepeti.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Exceptions
{  
    public class LocationIdMustNotBeNull : BaseException
    {
        public LocationIdMustNotBeNull() : base("Bu Id'ye sahip bir Lokasyon yok.") { }

    }
    
}
