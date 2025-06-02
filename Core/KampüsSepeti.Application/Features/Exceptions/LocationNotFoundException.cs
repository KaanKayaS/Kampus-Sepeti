using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Exceptions
{
    public class LocationNotFoundException : NotFoundException
    {
        public LocationNotFoundException() : base("Bu isimde şehir bulunamadı.") { }

    }
}
