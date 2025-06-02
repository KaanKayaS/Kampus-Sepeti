using AutoMapper;
using KampüsSepeti.Application.Features.Queries.CommentQueries;
using KampüsSepeti.Application.Features.Results.CommentResults;
using KampüsSepeti.Application.Features.Rules;
using KampüsSepeti.Application.Interfaces.UnitOfWorks;
using KampüsSepeti.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Handlers.CommentHandlers
{
    public class GetCommentsByAnnouncementIdQueryHandler : IRequestHandler<GetCommentsByAnnouncementIdQuery
        , IList<GetCommentsByAnnouncementIdQueryResult>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly AnnouncementRules announcementRules;

        public GetCommentsByAnnouncementIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,
            AnnouncementRules announcementRules)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.announcementRules = announcementRules;
        }
        public async Task<IList<GetCommentsByAnnouncementIdQueryResult>> Handle(GetCommentsByAnnouncementIdQuery request, CancellationToken cancellationToken)
        {
            Announcement announcement = await unitOfWork.GetReadRepository<Announcement>().GetAsync(x => x.Id== request.Id);
            await announcementRules.AnnouncementNotFound(announcement);

            IList<Comment> comments = await unitOfWork.GetReadRepository<Comment>()
                .GetAllAsync(x => x.AnnouncementId == request.Id, include: x => x.Include(u => u.User));


            return mapper.Map<IList<GetCommentsByAnnouncementIdQueryResult>>(comments);
        }
    }
}
