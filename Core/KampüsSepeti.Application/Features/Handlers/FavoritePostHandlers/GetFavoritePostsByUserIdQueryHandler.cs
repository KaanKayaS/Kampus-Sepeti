using AutoMapper;
using KampüsSepeti.Application.Features.Queries.FavoritePostQueries;
using KampüsSepeti.Application.Features.Results.FavoriteResults;
using KampüsSepeti.Application.Features.Rules;
using KampüsSepeti.Application.Interfaces.UnitOfWorks;
using KampüsSepeti.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace KampüsSepeti.Application.Features.Handlers.FavoritePostHandlers
{
    public class GetFavoritePostsByUserIdQueryHandler : IRequestHandler<GetFavoritePostsByUserIdQuery, IList<GetFavoritePostsByUserIdQueryResult>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserRules userRules;

        public GetFavoritePostsByUserIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor, UserRules userRules)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
            this.userRules = userRules;
        }

        public async Task<IList<GetFavoritePostsByUserIdQueryResult>> Handle(GetFavoritePostsByUserIdQuery request, CancellationToken cancellationToken)
        {
            User? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            await userRules.UserMustNotBeNull(user);

            IList<FavoritePost> favoritePosts = await unitOfWork.GetReadRepository<FavoritePost>().GetAllAsync(x => x.UserId == request.Id);

            var postIds = favoritePosts.Select(x => x.PostId).ToList();

            var values = await unitOfWork.GetReadRepository<Post>().GetAllAsync(x => postIds.Contains(x.Id));

            return mapper.Map<IList<GetFavoritePostsByUserIdQueryResult>>(values);
        }
    }
}
