using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;
using UNICAP.SiteCurso.Application.Interfaces;
using UNICAP.SiteCurso.Application.Jwt;
using UNICAP.SiteCurso.Application.Pipelines;

namespace UNICAP.SiteCurso.Application.IoC
{
    public static class ApplicationExtensions
    {

        public static IServiceCollection AppRegister(this IServiceCollection services, IConfiguration configuration)
        {
            var mapper = new MapperConfiguration(config =>
            {
                config.AddMaps(typeof(ApplicationExtensions).Assembly);
            });
            mapper.AssertConfigurationIsValid();
            var map = mapper.CreateMapper();
            services.AddSingleton(map);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationExtensions).Assembly));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionBehavior<,>));
            services.AddValidatorsFromAssembly(typeof(ApplicationExtensions).Assembly);
            services.AddTransient<Response>();
            services.AddTransient<ITokenGenerator, JwtGenerator>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationExtensions).Assembly));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionBehavior<,>));
            services.AddTransient<Response>();

            services.RegisterAssemblyPublicNonGenericClasses(typeof(ApplicationExtensions).Assembly)
                .Where(e => e.Name.EndsWith("ExceptionHandler"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Transient);

            services.RegisterAssemblyPublicNonGenericClasses(typeof(ApplicationExtensions).Assembly)
                .Where(e => e.Name.EndsWith("StrategyHandler"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Transient);

            return services;
        }
    }
}
