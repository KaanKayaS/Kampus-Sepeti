using KampüsSepeti.Application.Features.Queries.MyProfileQueries;
using KampüsSepeti.Application.Features.Rules;
using KampüsSepeti.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Handlers.MyProfileHandlers
{
    public class GetImagePathQueryHandler : IRequestHandler<GetImagePathQuery, string>
    {
        private readonly UserManager<User> userManager;
        private readonly UserRules userRules;

        public GetImagePathQueryHandler(UserManager<User> userManager, UserRules userRules)
        {
            this.userManager = userManager;
            this.userRules = userRules;
        }
        public async Task<string> Handle(GetImagePathQuery request, CancellationToken cancellationToken)
        {
            User? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            await userRules.UserMustNotBeNull(user);

            string path = user.Image;

            return path;
        }
    }
}
