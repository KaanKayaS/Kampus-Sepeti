using AutoMapper;
using KampüsSepeti.Application.Features.Queries.UniversityQueries;
using KampüsSepeti.Application.Features.Results.LocationResults;
using KampüsSepeti.Application.Features.Results.UniversityResults;
using KampüsSepeti.Application.Interfaces.UnitOfWorks;
using KampüsSepeti.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Handlers.UniversityHandlers
{
    public class GetAllUniversityQueryHandler : IRequestHandler<GetAllUniversityQuery, IList<GetAllUniversityQueryResult>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllUniversityQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IList<GetAllUniversityQueryResult>> Handle(GetAllUniversityQuery request, CancellationToken cancellationToken)
        {
           var values = await unitOfWork.GetReadRepository<University>().GetAllAsync();
            return mapper.Map<IList<GetAllUniversityQueryResult>>(values);
           
        }
    }
}
