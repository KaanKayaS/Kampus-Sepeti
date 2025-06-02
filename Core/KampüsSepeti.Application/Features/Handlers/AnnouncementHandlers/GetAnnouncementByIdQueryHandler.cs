using AutoMapper;
using KampüsSepeti.Application.Features.Queries.AnnouncementQueries;
using KampüsSepeti.Application.Features.Results.AnnouncementResults;
using KampüsSepeti.Application.Interfaces.UnitOfWorks;
using KampüsSepeti.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Handlers.AnnouncementHandlers
{
    public class GetAnnouncementByIdQueryHandler : IRequestHandler<GetAnnouncementByIdQuery, GetAnnouncementByIdQueryResult>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAnnouncementByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<GetAnnouncementByIdQueryResult> Handle(GetAnnouncementByIdQuery request, CancellationToken cancellationToken)
        {
            Announcement announcement = await unitOfWork.GetReadRepository<Announcement>()
               .GetAsync(x => x.Id == request.Id, include: x => x.Include(u => u.User)
                                                                 .Include(l => l.Location)
                                                                 .Include(c => c.Comments)
                                                                 .ThenInclude(u => u.User));   
            
            announcement.Comments = announcement.Comments.OrderByDescending(c => c.CreatedDate).ToList();
                                  

            return mapper.Map<GetAnnouncementByIdQueryResult>(announcement);
        }
    }
}
