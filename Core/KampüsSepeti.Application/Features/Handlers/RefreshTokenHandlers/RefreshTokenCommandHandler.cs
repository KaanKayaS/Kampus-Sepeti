using AutoMapper;
using KampüsSepeti.Application.Features.Commands.RefreshTokenCommands;
using KampüsSepeti.Application.Features.Results.RefreshTokenResults;
using KampüsSepeti.Application.Features.Rules;
using KampüsSepeti.Application.Interfaces.Tokens;
using KampüsSepeti.Application.Interfaces.UnitOfWorks;
using KampüsSepeti.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Handlers.RefreshTokenHandlers
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenCommandResult>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly ITokenService tokenService;
        private readonly RefreshTokenRules refreshTokenRules;

        public RefreshTokenCommandHandler(IMapper mapper, IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor, UserManager<User> userManager,
            ITokenService tokenService, RefreshTokenRules refreshTokenRules)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.tokenService = tokenService;
            this.refreshTokenRules = refreshTokenRules;
        }
        public async Task<RefreshTokenCommandResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var principal = tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
            string email = principal.FindFirstValue(ClaimTypes.Email);

            var user = await userManager.FindByNameAsync(email);
            var roles = await userManager.GetRolesAsync(user);

            await refreshTokenRules.RefreshTokenShouldNotBeExpired(user.RefreshTokenExpiryTime);

            JwtSecurityToken newAccessToken = await tokenService.CreateToken(user, roles);
            string NewRefreshToken = tokenService.GenerateRefreshToken();

            user.RefreshToken = NewRefreshToken;
            await userManager.UpdateAsync(user);

            return new RefreshTokenCommandResult
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = NewRefreshToken
            };
        }
    }
}
