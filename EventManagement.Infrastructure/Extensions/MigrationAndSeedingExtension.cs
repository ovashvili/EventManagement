using AutoMapper;
using EventManagement.Application.Contracts;
using EventManagement.Domain.Entities;
using EventManagement.Persistence.Context;
using EventManagement.Persistence.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EventManagement.Infrastructure.Extensions
{
    public static class MigrationAndSeedingExtension
    {
        public static async Task AutomateMigrationAndSeeding(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                var context = services.GetRequiredService<EventDbContext>();
                var userManager = services.GetRequiredService<UserManager<User>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userService = services.GetRequiredService<IUserService>();
                var mapper = services.GetRequiredService<IMapper>();
                context.Database.Migrate();
                await ContextSeed.SeedRolesAsync(userManager, roleManager).ConfigureAwait(false);
                await ContextSeed.SeedSudoAsync(userService, userManager).ConfigureAwait(false);

            }
        }
    }
}
