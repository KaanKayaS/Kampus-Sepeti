using KampüsSepeti.Application.Features.Commands.LocationCommands;
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
    public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, Unit>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly LocationRules locationRules;

        public CreateLocationCommandHandler(IUnitOfWork unitOfWork, LocationRules locationRules)
        {
            this.unitOfWork = unitOfWork;
            this.locationRules = locationRules;
        }
        public async Task<Unit> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            var locations = await unitOfWork.GetReadRepository<Location>().GetAllAsync();

            await locationRules.LocationTitleMustNotBeSame(locations,request.Name);

            await unitOfWork.GetWriteRepository<Location>().AddAsync(new Location
            {
                Name = request.Name
            });

            await unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
