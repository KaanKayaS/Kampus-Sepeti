using AutoMapper;
using KampüsSepeti.Application.DTOs;
using KampüsSepeti.Application.Features.Queries.PostQueries;
using KampüsSepeti.Application.Features.Results.PostResults;
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

namespace KampüsSepeti.Application.Features.Handlers.PostHandlers
{
    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, GetPostByIdQueryResult>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly PostRules postRules;

        public GetPostByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, PostRules postRules)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.postRules = postRules;
        }
        public async Task<GetPostByIdQueryResult> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var values = await unitOfWork.GetReadRepository<Post>().GetAsync(x => x.Id == request.Id,
               include: x => x.Include(x => x.Location)
                              .Include(x => x.Status)
                              .Include(x => x.User)
                              .Include(x => x.Category)
               );

            await postRules.PostNotFound(values);

            mapper.Map<LocationDto>(new Location());

            mapper.Map<StatusDto>(new Status());

            mapper.Map<UserDto>(new User());

            mapper.Map<CategoryDto>(new Category());

            return mapper.Map<GetPostByIdQueryResult>(values);
        }
    }
}
