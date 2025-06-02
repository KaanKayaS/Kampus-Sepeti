using KampüsSepeti.Application.Features.Commands.RevokeCommands;
using KampüsSepeti.Application.Features.Rules;
using KampüsSepeti.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Handlers.RevokeHandlers
{
    public class RevokeCommandHandler : IRequestHandler<RevokeCommand, Unit>
    {
        private readonly UserManager<User> userManager;
        private readonly RevokeRules revokeRules;

        public RevokeCommandHandler(UserManager<User> userManager,RevokeRules revokeRules)
        {
            this.userManager = userManager;
            this.revokeRules = revokeRules;
        }
        public async Task<Unit> Handle(RevokeCommand request, CancellationToken cancellationToken)
        {
            User? user = await userManager.FindByEmailAsync(request.Email);
            await revokeRules.EmailAddressShouldBeValid(user);

            user.RefreshToken = null;
            await userManager.UpdateAsync(user);

            return Unit.Value;
        }
    }
}
