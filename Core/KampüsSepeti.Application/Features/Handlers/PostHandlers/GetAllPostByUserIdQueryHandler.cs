using AutoMapper;
using KampüsSepeti.Application.Features.Queries.PostQueries;
using KampüsSepeti.Application.Features.Results.PostResults;
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

namespace KampüsSepeti.Application.Features.Handlers.PostHandlers
{
    public class GetAllPostByUserIdQueryHandler : IRequestHandler<GetAllPostByUserIdQuery, IList<GetAllPostByUserIdQueryResult>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly UserRules userRules;

        public GetAllPostByUserIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,
            UserManager<User> userManager, UserRules userRules )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.userManager = userManager;
            this.userRules = userRules;
        }
        public async Task<IList<GetAllPostByUserIdQueryResult>> Handle(GetAllPostByUserIdQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

            await userRules.UserMustNotBeNull(user);

            var values = await unitOfWork.GetReadRepository<Post>().GetAllAsync(x => x.UserId == request.Id);

            return mapper.Map<IList<GetAllPostByUserIdQueryResult>>(values);
        }
    }
}
