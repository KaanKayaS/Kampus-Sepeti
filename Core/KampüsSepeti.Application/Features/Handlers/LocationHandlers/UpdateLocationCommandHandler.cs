using AutoMapper;
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
    public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly LocationRules locationRules;

        public UpdateLocationCommandHandler(IUnitOfWork unitOfWork, LocationRules locationRules)
        {
            this.unitOfWork = unitOfWork;
            this.locationRules = locationRules;
        }
        public async Task Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var value = await unitOfWork.GetReadRepository<Location>().GetAsync(x => x.Id == request.Id);
           
            await locationRules.LocationNotFound(value);

            value.Name = request.Name;
            await unitOfWork.GetWriteRepository<Location>().UpdateAsync(value);
                   
            await unitOfWork.SaveAsync();

        }
    }
}
