using KampüsSepeti.Application.Features.Commands.LocationCommands;
using KampüsSepeti.Application.Features.Rules;
using KampüsSepeti.Application.Interfaces.UnitOfWorks;
using KampüsSepeti.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Handlers.LocationHandlers
{
    public class RemoveLocationCommandHandler : IRequestHandler<RemoveLocationCommand, Unit>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly LocationRules locationRules;

        public RemoveLocationCommandHandler(IUnitOfWork unitOfWork, LocationRules locationRules)
        {
            this.unitOfWork = unitOfWork;
            this.locationRules = locationRules;
        }
        public async Task<Unit> Handle(RemoveLocationCommand request, CancellationToken cancellationToken)
        {
            var value = await unitOfWork.GetReadRepository<Location>().GetAsync(x => x.Id == request.Id);

            await locationRules.LocationNotFound(value);

            await unitOfWork.GetWriteRepository<Location>().HardDeleteAsync(value);

            await unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
