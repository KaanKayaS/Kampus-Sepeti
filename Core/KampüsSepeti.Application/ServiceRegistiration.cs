using AutoMapper;
using FluentValidation;
using KampüsSepeti.Application.AutoMapper;
using KampüsSepeti.Application.Beheviors;
using KampüsSepeti.Application.Exceptions;
using KampüsSepeti.Application.Features.Rules;
using KampüsSepeti.Application.Features.Validator;
using KampüsSepeti.Application.Interfaces.Service;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application
{
    public static class ServiceRegistiration
    {
        public static void AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistiration).Assembly));

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddTransient<ExceptionMiddleware>();
            services.AddTransient<LocationRules>();  // cem keskin 15.video dk 20 daha pratik bir yapı var
            services.AddTransient<RegisterRules>();
            services.AddTransient<LoginRules>();
            services.AddTransient<RefreshTokenRules>();
            services.AddTransient<RevokeRules>();
            services.AddTransient<UserRules>();
            services.AddTransient<PostRules>();
            services.AddTransient<FavoritePostRules>();
            services.AddTransient<AnnouncementRules>();
            services.AddTransient<CommentRules>();
            services.AddTransient<UserFollowersRules>();


            services.AddValidatorsFromAssemblyContaining<CreateLocationCommandValidator>();
            ValidatorOptions.Global.LanguageManager.Culture = new System.Globalization.CultureInfo("tr"); // fluent validation tr yapma

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));


            services.AddSignalR();
        }
    }
}
