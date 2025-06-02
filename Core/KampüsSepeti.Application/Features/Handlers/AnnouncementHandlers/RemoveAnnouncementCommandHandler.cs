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
    public class RemoveAnnouncementCommandHandler : IRequestHandler<RemoveAnnouncementCommand, Unit>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly AnnouncementRules announcementRules;

        public RemoveAnnouncementCommandHandler(IUnitOfWork unitOfWork, AnnouncementRules announcementRules)
        {
            this.unitOfWork = unitOfWork;
            this.announcementRules = announcementRules;
        }
        public async Task<Unit> Handle(RemoveAnnouncementCommand request, CancellationToken cancellationToken)
        {
            Announcement announcement = await unitOfWork.GetReadRepository<Announcement>()
                .GetAsync(x => x.Id == request.Id);

            await announcementRules.AnnouncementNotFound(announcement);

            await unitOfWork.GetWriteRepository<Announcement>().HardDeleteAsync(announcement);

            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
