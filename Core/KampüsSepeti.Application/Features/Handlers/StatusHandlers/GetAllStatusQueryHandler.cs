using KampüsSepeti.Application.Features.Queries.StatusQueries;
using KampüsSepeti.Application.Features.Results.StatusResults;
using KampüsSepeti.Application.Interfaces.UnitOfWorks;
using KampüsSepeti.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Handlers.StatusHandler
{
    public class GetAllStatusQueryHandler : IRequestHandler<GetAllStatusQuery, IList<GetAllStatusQueryResult>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAllStatusQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IList<GetAllStatusQueryResult>> Handle(GetAllStatusQuery request, CancellationToken cancellationToken)
        {
            var values = await unitOfWork.GetReadRepository<Status>().GetAllAsync();
            return values.Select(x => new GetAllStatusQueryResult
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
    }
}
