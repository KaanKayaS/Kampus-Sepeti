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
    public class LocationRules : BaseRules
    {
        public Task LocationTitleMustNotBeSame(IList<Location> locations ,string requestTitle)
        {
            if (locations.Any(x => x.Name.Equals(requestTitle, StringComparison.OrdinalIgnoreCase)))
                throw new LocationTitleMustNotBeSameException();

            return Task.CompletedTask;
        }

        public Task LocationNotFound(Location location)
        {
            if (location == null)
                throw new LocationNotFoundException();
            return Task.CompletedTask;

        }
    }
}
