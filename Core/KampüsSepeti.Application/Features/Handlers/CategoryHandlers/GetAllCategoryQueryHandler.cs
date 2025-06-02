using AutoMapper;
using KampüsSepeti.Application.Features.Queries.CategoriesQueries;
using KampüsSepeti.Application.Features.Results.CategoryResults;
using KampüsSepeti.Application.Interfaces.UnitOfWorks;
using KampüsSepeti.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Handlers.CategoryHandlers
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, IList<GetAllCategoryQueryResult>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IList<GetAllCategoryQueryResult>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var values = await unitOfWork.GetReadRepository<Category>().GetAllAsync();

            return mapper.Map<IList<GetAllCategoryQueryResult>>(values);
        }
    }
}
