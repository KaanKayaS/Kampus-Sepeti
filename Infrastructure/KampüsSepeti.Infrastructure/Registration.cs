﻿using KampüsSepeti.Application.Interfaces.Tokens;
using KampüsSepeti.Infrastructure.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Infrastructure
{
    public static class Registration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
        { 
            services.Configure<TokenSettings>(configuration.GetSection("JWT"));
            services.AddTransient<ITokenService, TokenService>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
            {
                opt.SaveToken = true;
                opt.MapInboundClaims = false;
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
                    ValidateLifetime = false,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    ClockSkew = TimeSpan.Zero,
                };
            });

        }
    }
}
