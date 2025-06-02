using AutoMapper;
using KampüsSepeti.Application.Features.Queries.UserQueries;
using KampüsSepeti.Application.Features.Results.UserResults;
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

namespace KampüsSepeti.Application.Features.Handlers.UserHandlers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery,IList<GetAllUsersQueryResult>>
    {
        private readonly UserManager<User> userManager;
        private readonly UserRules userRules;
        private readonly IMapper mapper;

        public GetAllUsersQueryHandler(UserManager<User> userManager, UserRules userRules, IMapper mapper)
        {
            this.userManager = userManager;
            this.userRules = userRules;
            this.mapper = mapper;
        }

        public async Task<IList<GetAllUsersQueryResult>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            IList<User> users = await userManager.Users.ToListAsync();

            return mapper.Map<IList<GetAllUsersQueryResult>>(users);
        }
    }
}
