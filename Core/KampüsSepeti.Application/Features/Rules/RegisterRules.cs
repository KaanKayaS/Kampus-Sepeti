using KampüsSepeti.Application.Bases;
using KampüsSepeti.Application.Features.Exceptions;
using KampüsSepeti.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Rules
{
    public class RegisterRules : BaseRules
    {
        public Task UserShouldNotBeExist(User? user)
        {
            if (user is not null)
                throw new UserAlreadyExistException();

            return Task.CompletedTask;
        }

        public Task MustBeLocationId(IList<Location> locations ,int id)
        {
            if (!locations.Any(x => x.Id == id))
                throw new LocationIdMustNotBeNull();

            return Task.CompletedTask;
        }

        public Task MustBeUniId(IList<University> universities, int id)
        {

            if (!universities.Any(x => x.Id == id))
                throw new UniversityIdMustNotBeNull();

            return Task.CompletedTask;
        }

        public Task FullNameMustBeUnique(User? user)
        {
            if (user != null)
                throw new FullNameMustBeUniqueException();

            return Task.CompletedTask;
        }

        public Task PhoneNumberMustBeUnique(User? user)
        {
            if (user != null)
                throw new PhoneNumberMustBeUniqueException();

            return Task.CompletedTask;
        }
    }
}
