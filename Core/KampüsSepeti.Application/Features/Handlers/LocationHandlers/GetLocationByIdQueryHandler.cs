using AutoMapper;
using KampüsSepeti.Application.Features.Queries.LocationQueries;
using KampüsSepeti.Application.Features.Results.LocationResults;
using KampüsSepeti.Application.Features.Rules;
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
    public class GetLocationByIdQueryHandler : IRequestHandler<GetLocationByIdQuery, GetLocationByIdQueryResult>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly LocationRules locationRules;

        public GetLocationByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, LocationRules locationRules)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.locationRules = locationRules;
        }
        public async Task<GetLocationByIdQueryResult> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
        {
            var value = await unitOfWork.GetReadRepository<Location>().GetAsync( x => x.Id == request.Id );

            await locationRules.LocationNotFound(value);

            var map = mapper.Map<GetLocationByIdQueryResult>(value);
            
            return map;
        }
    }
}
