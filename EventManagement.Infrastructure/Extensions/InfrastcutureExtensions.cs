using EventManagement.Application.Contracts;
using EventManagement.Application.Users.Commands.AuthenticateUser;
using EventManagement.Infrastructure.Helpers;
using EventManagement.Infrastructure.Helpers.JWT;
using EventManagement.Infrastructure.Helpers.Utilities;
using EventManagement.Infrastructure.Mappings;
using EventManagement.Infrastructure.Services;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace EventManagement.Infrastructure.Extensions
{
    public static class InfrastcutureExtensions
    {
        public static IServiceCollection InstallInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleManagerService, RoleManagerService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IUtility, Utility>();
            services.AddScoped<IConfigurationValueService, ConfigurationValueService>();
            services.AddMediatR(typeof(AuthenticateUserCommand).Assembly);
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.Configure<JWTConfiguration>(configuration.GetSection(nameof(JWTConfiguration)));
            services.Configure<DirectoriesPathOptions>(configuration.GetSection(DirectoriesPathOptions.SectionName));

            services.AddStackExchangeRedisCache(options =>
            {
                var connection = configuration.GetConnectionString("Redis");
                options.Configuration = connection;
            });
            services.AddHttpContextAccessor();

            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("User", policy =>
                {
                    policy.RequireRole("Basic", "Moderator", "Admin");
                });
            });
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("Moderator", policy =>
                {
                    policy.RequireRole("Moderator", "Admin");
                });
            });

            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("Admin", policy =>
                {
                    policy.RequireRole("Admin");
                });
            });

            return services;
        }

    }
}
