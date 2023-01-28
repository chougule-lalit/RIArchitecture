using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using RIArchitecture.Api.Errors;
using RIArchitecture.Application;
using RIArchitecture.Application.Administration.Services;
using RIArchitecture.Application.Contracts;
using RIArchitecture.Application.Contracts.Administration.Interfaces;
using RIArchitecture.Application.Contracts.Utility;
using RIArchitecture.Application.Contracts.Utility.Otp;
using RIArchitecture.Application.Utility;
using RIArchitecture.Core.RIArchitectureCoreBase.Interface;
using RIArchitecture.Infrastructure.RIArchitectureDomainService;
using RIArchitecture.Infrastructure.RIArchitectureInfrastructureBase;
using System.Linq;
using System.Reflection;

namespace RIArchitecture.Api
{
    public static class RIArchitectureDIHandler
    {
        public static void AddRIArchitectureDependencies(this IServiceCollection services)
        {
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped(typeof(IRepository<>), (typeof(Repository<>)));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IUserRoleAppService, UserRoleAppService>();
            services.AddTransient<IUserAppService, UserAppService>();
            services.AddTransient<IRoleAppService, RoleAppService>();
            services.AddTransient<IItemAppService, ItemAppService>();
            services.AddTransient<IOtpDetailAppService, OtpDetailAppService>();
            services.AddTransient<IEmailAppService, EmailAppService>();
            services.AddTransient<IFileUploadAppService, FileUploadAppService>();
            //services.AddTransient<IExcelSeederAppService, ExcelSeederAppService>();


            //Configuring Global Exception handler options for ValidationErrors
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });
        }

        public static void AddAllTypes<T>(this IServiceCollection services
            , Assembly[] assemblies
            , bool additionalRegisterTypesByThemself = false
            , ServiceLifetime lifetime = ServiceLifetime.Transient
        )
        {
            var typesFromAssemblies = assemblies.SelectMany(a =>
                a.DefinedTypes.Where(x => x.GetInterfaces().Any(i => i == typeof(T))));
            foreach (var type in typesFromAssemblies)
            {
                services.Add(new ServiceDescriptor(typeof(T), type, lifetime));
                if (additionalRegisterTypesByThemself)
                    services.Add(new ServiceDescriptor(type, type, lifetime));
            }
        }
    }
}
