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
    public class UpdateTruePhoneVisibleCommandHandler : IRequestHandler<UpdateTruePhoneVisibleCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserRules userRules;
        private readonly UserManager<User> userManager;

        public UpdateTruePhoneVisibleCommandHandler(IUnitOfWork unitOfWork, UserRules userRules, UserManager<User> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userRules = userRules;
            this.userManager = userManager;
        }
        public async Task<bool> Handle(UpdateTruePhoneVisibleCommand request, CancellationToken cancellationToken)
        {
            User? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            await userRules.UserMustNotBeNull(user);

            user.IsPhoneVisible = true;

            await unitOfWork.GetWriteRepository<User>().UpdateAsync(user);
            await unitOfWork.SaveAsync();

            return true;
        }
    }
}
