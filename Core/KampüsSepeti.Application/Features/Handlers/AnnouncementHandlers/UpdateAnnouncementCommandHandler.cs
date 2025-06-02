using KampüsSepeti.Application.Features.Commands.AnnouncementCommands;
using KampüsSepeti.Application.Features.Rules;
using KampüsSepeti.Application.Interfaces.UnitOfWorks;
using KampüsSepeti.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Handlers.AnnouncementHandlers
{
    public class UpdateAnnouncementCommandHandler : IRequestHandler<UpdateAnnouncementCommand, Unit>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly AnnouncementRules announcementRules;
        private readonly LocationRules locationRules;

        public UpdateAnnouncementCommandHandler(IUnitOfWork unitOfWork, AnnouncementRules announcementRules
            , LocationRules locationRules)
        {
            this.unitOfWork = unitOfWork;
            this.announcementRules = announcementRules;
            this.locationRules = locationRules;
        }
        public async Task<Unit> Handle(UpdateAnnouncementCommand request, CancellationToken cancellationToken)
        {
            Announcement announcement = await unitOfWork.GetReadRepository<Announcement>()
                .GetAsync(x => x.Id == request.Id);

            Location location = await unitOfWork.GetReadRepository<Location>().GetAsync(x => x.Id == request.LocationId);
            await locationRules.LocationNotFound(location);

            await announcementRules.AnnouncementNotFound(announcement);

            announcement.Title = request.Title;
            announcement.Description = request.Description;
            announcement.LocationId = request.LocationId;

            await unitOfWork.GetWriteRepository<Announcement>().UpdateAsync(announcement);

            await unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
