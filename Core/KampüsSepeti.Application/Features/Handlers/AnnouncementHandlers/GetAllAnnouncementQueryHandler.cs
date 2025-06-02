using AutoMapper;
using KampüsSepeti.Application.Features.Queries.AnnouncementQueries;
using KampüsSepeti.Application.Features.Results.AnnouncementResults;
using KampüsSepeti.Application.Features.Results.PostResults;
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
    public class GetAllAnnouncementQueryHandler : IRequestHandler<GetAllAnnouncementQuery, IList<GetAllAnnouncementQueryResult>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllAnnouncementQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IList<GetAllAnnouncementQueryResult>> Handle(GetAllAnnouncementQuery request, CancellationToken cancellationToken)
        {
            IList<Announcement> values = await unitOfWork.GetReadRepository<Announcement>()
                .GetAllAsync(include: x => x.Include(u => u.User)
                                            .Include(l => l.Location)
                                            .Include(c => c.Comments));

            return mapper.Map<IList<GetAllAnnouncementQueryResult>>(values);

        }
    }
}
