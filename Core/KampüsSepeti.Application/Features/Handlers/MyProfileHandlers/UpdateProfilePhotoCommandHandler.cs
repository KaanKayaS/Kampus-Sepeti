using KampüsSepeti.Application.Features.Commands.MyProfileCommands;
using KampüsSepeti.Application.Features.Rules;
using KampüsSepeti.Application.Interfaces.UnitOfWorks;
using KampüsSepeti.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KampüsSepeti.Application.Interfaces.Service;

namespace KampüsSepeti.Application.Features.Handlers.MyProfileHandlers
{
    public class UpdateProfilePhotoCommandHandler : IRequestHandler<UpdateProfilePhotoCommand, Unit>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<User> userManager;
        private readonly UserRules userRules;
        private readonly IFileService fileService;

        public UpdateProfilePhotoCommandHandler(IUnitOfWork unitOfWork, UserManager<User> userManager
            , UserRules userRules,IFileService fileService)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.userRules = userRules;
            this.fileService = fileService;
        }

        public async Task<Unit> Handle(UpdateProfilePhotoCommand request, CancellationToken cancellationToken)
        {
            User? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            await userRules.UserMustNotBeNull(user);

            var path = await fileService.SaveProfilePhotoAsync(request.ProfilePhoto);
            user.Image = path;

            await unitOfWork.GetWriteRepository<User>().UpdateAsync(user);

            await unitOfWork.SaveAsync();
            return Unit.Value;
           
        }
    }
}
