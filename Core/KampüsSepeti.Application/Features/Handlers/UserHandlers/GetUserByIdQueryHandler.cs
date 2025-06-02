using KampüsSepeti.Application.Features.Queries.UserQueries;
using KampüsSepeti.Application.Features.Results.UserResults;
using KampüsSepeti.Application.Features.Rules;
using KampüsSepeti.Application.Interfaces.UnitOfWorks;
using KampüsSepeti.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Handlers.UserHandlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdQueryResult>
    {
        private readonly UserManager<User> userManager;
        private readonly UserRules userRules;

        public GetUserByIdQueryHandler(UserManager<User> userManager,UserRules userRules)
        {
            this.userManager = userManager;
            this.userRules = userRules;
        }
        public async Task<GetUserByIdQueryResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            await userRules.UserMustNotBeNull(user);

            return new GetUserByIdQueryResult
            {
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber  ,
                IsPhoneVisible = user.IsPhoneVisible,
                Image = user.Image
            };
        }
    }
}
