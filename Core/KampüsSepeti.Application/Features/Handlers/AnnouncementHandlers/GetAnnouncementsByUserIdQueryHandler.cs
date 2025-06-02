using AutoMapper;
using KampüsSepeti.Application.Features.Queries.AnnouncementQueries;
using KampüsSepeti.Application.Features.Results.AnnouncementResults;
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

namespace KampüsSepeti.Application.Features.Handlers.AnnouncementHandlers
{
    public class GetAnnouncementsByUserIdQueryHandler : IRequestHandler<GetAnnouncementsByUserIdQuery, IList<GetAnnouncementsByUserIdQueryResult>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly UserRules userRules;

        public GetAnnouncementsByUserIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper
            ,UserManager<User> userManager, UserRules userRules)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.userManager = userManager;
            this.userRules = userRules;
        }
        public async Task<IList<GetAnnouncementsByUserIdQueryResult>> Handle(GetAnnouncementsByUserIdQuery request, CancellationToken cancellationToken)
        {
            User? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            await userRules.UserMustNotBeNull(user);

            IList<Announcement> announcements = await unitOfWork.GetReadRepository<Announcement>()
                .GetAllAsync(x => x.UserId == request.Id, include: x => x.Include(c => c.Comments)
                                                                         .Include(l => l.Location));

            return mapper.Map<IList<GetAnnouncementsByUserIdQueryResult>>(announcements);
        }
    }
}
