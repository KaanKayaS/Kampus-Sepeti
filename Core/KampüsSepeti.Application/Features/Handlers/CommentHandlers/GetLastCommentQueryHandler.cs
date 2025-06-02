using AutoMapper;
using KampüsSepeti.Application.Features.Queries.CommentQueries;
using KampüsSepeti.Application.Features.Results.AnnouncementResults;
using KampüsSepeti.Application.Features.Results.CommentResults;
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

namespace KampüsSepeti.Application.Features.Handlers.CommentHandlers
{
    public class GetLastCommentQueryHandler : IRequestHandler<GetLastCommentQuery, GetLastCommentQueryResult>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly CommentRules commentRules;
        private readonly UserRules userRules;
        private readonly AnnouncementRules announcementRules;
        private readonly UserManager<User> userManager;

        public GetLastCommentQueryHandler(IUnitOfWork unitOfWork, IMapper mapper
            , CommentRules commentRules, UserRules userRules , AnnouncementRules announcementRules , UserManager<User> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.commentRules = commentRules;
            this.userRules = userRules;
            this.announcementRules = announcementRules;
            this.userManager = userManager;
        }
        public async Task<GetLastCommentQueryResult> Handle(GetLastCommentQuery request, CancellationToken cancellationToken)
        {
            User? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            await userRules.UserMustNotBeNull(user);

            Announcement announcement = await unitOfWork.GetReadRepository<Announcement>().GetAsync(x => x.Id == request.AnnouncementId);
            await announcementRules.AnnouncementNotFound(announcement);
            
            IList<Comment> comment = await unitOfWork.GetReadRepository<Comment>()
                .GetAllAsync(x => x.UserId == request.UserId && x.AnnouncementId == request.AnnouncementId
                , include: x=> x.Include(u => u.User));


            Comment? lastComment = comment.OrderByDescending(x => x.CreatedDate)
                                     .FirstOrDefault();

            await commentRules.CommentNoutFound(lastComment);

            return mapper.Map<GetLastCommentQueryResult>(lastComment);  
        }
    }
}
