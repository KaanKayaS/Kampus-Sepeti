using AutoMapper;
using KampüsSepeti.Application.Features.Commands.RegisterCommands;
using KampüsSepeti.Application.Features.Rules;
using KampüsSepeti.Application.Interfaces.UnitOfWorks;
using KampüsSepeti.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Handlers.RegisterHandlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly RegisterRules registerRules;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public RegisterCommandHandler(IMapper mapper,IUnitOfWork unitOfWork
            ,IHttpContextAccessor httpContextAccessor, RegisterRules registerRules,UserManager<User> userManager,RoleManager<Role> roleManager)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
            this.registerRules = registerRules;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await registerRules.UserShouldNotBeExist(await userManager.FindByEmailAsync(request.Email));

            var locations = await unitOfWork.GetReadRepository<Location>().GetAllAsync();
            var universities = await unitOfWork.GetReadRepository<University>().GetAllAsync();
            var User = await userManager.Users.FirstOrDefaultAsync(x => x.FullName == request.FullName);
            var user2 = await userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);

            await registerRules.MustBeLocationId(locations, request.LocationId);
            await registerRules.MustBeUniId(universities, request.UniversityId);
            await registerRules.FullNameMustBeUnique(User);
            await registerRules.PhoneNumberMustBeUnique(user2);

            User user = mapper.Map<User>(request);
            user.UserName = request.Email;
            user.IsPhoneVisible = true;
            user.SecurityStamp = Guid.NewGuid().ToString(); // update işlemlerinde milisaniyelik işlemleri ayarlamak için kullanılır 

            IdentityResult result = await userManager.CreateAsync(user, request.Password);
            if (result.Succeeded) 
            {
                if (!await roleManager.RoleExistsAsync("user"))
                    await roleManager.CreateAsync(new Role
                    {
                        Name = "user",
                        NormalizedName = "USER",
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                    });

                await userManager.AddToRoleAsync(user, "user");
            }

            return Unit.Value;
        }
    }
}
