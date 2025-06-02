using KampüsSepeti.Application.Features.Commands.AnnouncementCommands;
using KampüsSepeti.Application.Features.Rules;
using KampüsSepeti.Application.Interfaces.UnitOfWorks;
using KampüsSepeti.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Handlers.AnnouncementHandlers
{
    public class CreateAnnouncementCommandHandler : IRequestHandler<CreateAnnouncementCommand, Unit>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserRules userRules;
        private readonly UserManager<User> userManager;
        private readonly LocationRules locationRules;
        private readonly AnnouncementRules announcementRules;

        public CreateAnnouncementCommandHandler(IUnitOfWork unitOfWork, 
            UserRules userRules, UserManager<User> userManager, LocationRules locationRules
            ,AnnouncementRules announcementRules)
        {
            this.unitOfWork = unitOfWork;
            this.userRules = userRules;
            this.userManager = userManager;
            this.locationRules = locationRules;
            this.announcementRules = announcementRules;
        }
        public async Task<Unit> Handle(CreateAnnouncementCommand request, CancellationToken cancellationToken)
        {
            User? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            await userRules.UserMustNotBeNull(user);

            Location location = await unitOfWork.GetReadRepository<Location>().GetAsync(x=> x.Id == request.LocationId);
            await locationRules.LocationNotFound(location);

            IList<Announcement> announcements = await unitOfWork.GetReadRepository<Announcement>().GetAllAsync();
            await announcementRules.AnnouncementTitleMustNotBeSame(announcements, request.Title);

            await unitOfWork.GetWriteRepository<Announcement>().AddAsync(new Announcement
            {
                UserId = request.UserId,
                LocationId = request.LocationId,
                Description = request.Description,
                Title = request.Title,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,      
            });

            await unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
