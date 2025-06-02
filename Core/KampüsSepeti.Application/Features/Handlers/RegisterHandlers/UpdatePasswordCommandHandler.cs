using AutoMapper;
using KampüsSepeti.Application.Features.Commands.RegisterCommands;
using KampüsSepeti.Application.Features.Rules;
using KampüsSepeti.Application.Interfaces.UnitOfWorks;
using KampüsSepeti.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Handlers.RegisterHandlers
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, Unit>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly UserRules userRules;

        public UpdatePasswordCommandHandler(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, UserRules userRules)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.userRules = userRules;
        }
        public async Task<Unit> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);
            await userRules.UserMustNotBeNull(user);

            bool isPasswordValid = await userManager.CheckPasswordAsync(user, request.OldPassword);
            await userRules.UserOldPasswordWrong(isPasswordValid);

            await userRules.UserPasswordNotBeSameOldPassword(request.Password, request.OldPassword);

            await userRules.UserNewPasswordsDoesNotMatch(request.Password, request.ConfirmPassword);


           var result = await userManager.ChangePasswordAsync(user, request.OldPassword, request.Password);

            if(!result.Succeeded) 
            {
                var errorMessages = result.Errors.Select(e => e.Description);
                var fullErrorMessage = string.Join(" | ", errorMessages);
                throw new Exception($"Parola güncellenemedi: {fullErrorMessage}");
            }

            user.RefreshToken = null;
            await userManager.UpdateAsync(user);

            return Unit.Value;
        }
    }
}
