using KampüsSepeti.Application.Features.Commands.MyProfileCommands;
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

namespace KampüsSepeti.Application.Features.Handlers.MyProfileHandlers
{
    public class UpdateNullProfilePhotoCommandHandler : IRequestHandler<UpdateNullProfilePhotoCommand, Unit>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<User> userManager;
        private readonly UserRules userRules;

        public UpdateNullProfilePhotoCommandHandler(IUnitOfWork unitOfWork, UserManager<User> userManager, UserRules userRules)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.userRules = userRules;
        }
        public async Task<Unit> Handle(UpdateNullProfilePhotoCommand request, CancellationToken cancellationToken)
        {
            User? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            await userRules.UserMustNotBeNull(user);
            await userRules.UserProfilePhotoAlreadyNull(user);

            user.Image = null;

            await unitOfWork.GetWriteRepository<User>().UpdateAsync(user);

            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
