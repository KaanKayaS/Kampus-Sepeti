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
    public class GetPostByLocationIdQueryHandler : IRequestHandler<GetPostByLocationIdQuery, IList<GetPostByLocationIdQueryResult>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetPostByLocationIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IList<GetPostByLocationIdQueryResult>> Handle(GetPostByLocationIdQuery request, CancellationToken cancellationToken)
        {
            var values = await unitOfWork.GetReadRepository<Post>().GetAllAsync(x => x.LocationId == request.Id ,
                include: x => x.Include(x => x.Location)
                               .Include(x => x.Status)
                               .Include(x => x.User)
                               .Include(x => x.Category)
                );
                 
                                       

            mapper.Map<LocationDto>(new Location()); 

            mapper.Map<StatusDto>(new Status());

            mapper.Map<UserDto>(new User());

            mapper.Map<CategoryDto>(new Category());

            return mapper.Map<IList<GetPostByLocationIdQueryResult>>(values);
        }
    }
}
