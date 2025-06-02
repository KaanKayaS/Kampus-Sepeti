using KampüsSepeti.Application.Features.Commands.CommentCommands;
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

namespace KampüsSepeti.Application.Features.Handlers.CommentHandlers
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Unit>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserRules userRules;
        private readonly UserManager<User> userManager;
        private readonly AnnouncementRules announcementRules;

        public CreateCommentCommandHandler(IUnitOfWork unitOfWork,
            UserRules userRules, UserManager<User> userManager
            , AnnouncementRules announcementRules)
        {
            this.unitOfWork = unitOfWork;
            this.userRules = userRules;
            this.userManager = userManager;
            this.announcementRules = announcementRules;
        }
        public async Task<Unit> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            User? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            await userRules.UserMustNotBeNull(user);

            Announcement announcement = await unitOfWork.GetReadRepository<Announcement>().GetAsync(x => x.Id == request.AnnouncementId);
            await announcementRules.AnnouncementNotFound(announcement);

            await unitOfWork.GetWriteRepository<Comment>().AddAsync(new Comment
            {
                AnnouncementId = request.AnnouncementId,
                UserId = request.UserId,
                Content = request.Content,
                CreatedDate = DateTime.UtcNow.AddHours(3),
                IsDeleted = false                           
            });

            await unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
