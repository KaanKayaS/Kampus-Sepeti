using AutoMapper;
using KampüsSepeti.Application.Features.Commands.LoginCommands;
using KampüsSepeti.Application.Features.Exceptions;
using KampüsSepeti.Application.Features.Results.LoginResults;
using KampüsSepeti.Application.Features.Rules;
using KampüsSepeti.Application.Interfaces.Tokens;
using KampüsSepeti.Application.Interfaces.UnitOfWorks;
using KampüsSepeti.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Handlers.LoginHandlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResult>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly LoginRules loginRules;
        private readonly ITokenService tokenService;
        private readonly IConfiguration configuration;

        public LoginCommandHandler(IMapper mapper, IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager, LoginRules loginRules,
            ITokenService tokenService, IConfiguration configuration)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.loginRules = loginRules;
            this.tokenService = tokenService;
            this.configuration = configuration;
        }
        public async Task<LoginCommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User user = await userManager.FindByEmailAsync(request.Email);
            bool checkPassword = await userManager.CheckPasswordAsync(user, request.Password);

            await loginRules.FailEmailOrPassword(user, checkPassword);

            var roles = await userManager.GetRolesAsync(user);

            JwtSecurityToken token = await tokenService.CreateToken(user, roles);
            string refreshToken = tokenService.GenerateRefreshToken();

            int.TryParse(configuration["JWT:RefreshTokenValidityInDays"], out int RefreshTokenValidityInDays);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(RefreshTokenValidityInDays);

            await userManager.UpdateAsync(user);
            await userManager.UpdateSecurityStampAsync(user);

            string _token = new JwtSecurityTokenHandler().WriteToken(token);

            await userManager.SetAuthenticationTokenAsync(user, "Default", "AccessToken", _token);

            return new()
            {
                Token = _token,
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            };

        }
    }
}
