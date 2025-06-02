using AutoMapper;
using KampüsSepeti.Application.DTOs;
using KampüsSepeti.Application.Features.Queries.PostQueries;
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

namespace KampüsSepeti.Application.Features.Handlers.PostHandlers
{
    public class GetAllPostQueryHandler : IRequestHandler<GetAllPostQuery, IList<GetAllPostQueryResult>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllPostQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IList<GetAllPostQueryResult>> Handle(GetAllPostQuery request, CancellationToken cancellationToken)
        {
            var values = await unitOfWork.GetReadRepository<Post>().GetAllAsync(include: x => x.Include(x => x.Location)
                                                                                               .Include(x => x.User));
                                                                                                
            var map = mapper.Map<LocationDto>(new Location());

            return mapper.Map<IList<GetAllPostQueryResult>>(values);

        }
    }
}
