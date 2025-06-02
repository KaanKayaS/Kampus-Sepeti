using AutoMapper;
using KampüsSepeti.Application.Features.Queries.LocationQueries;
using KampüsSepeti.Application.Features.Results.LocationResults;
using KampüsSepeti.Application.Interfaces.UnitOfWorks;
using KampüsSepeti.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Handlers.LocationHandlers
{
    public class GetAllLocationQueryHandler : IRequestHandler<GetAllLocationQuery, IList<GetAllLocationQueryResult>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllLocationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IList<GetAllLocationQueryResult>> Handle(GetAllLocationQuery request, CancellationToken cancellationToken)
        {
            var values = await unitOfWork.GetReadRepository<Location>().GetAllAsync();

            return mapper.Map<IList<GetAllLocationQueryResult>>(values);
        }
    }
}
