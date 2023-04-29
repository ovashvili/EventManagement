using EventManagement.Application.Contracts;
using EventManagement.Application.Users.Commands.RegisterUser;
using EventManagement.Domain;
using EventManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EventManagement.Persistence.Seed
{
    public class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString())).ConfigureAwait(false);
            await roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString())).ConfigureAwait(false);
            await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString())).ConfigureAwait(false);
        }
        public static async Task SeedSudoAsync(IUserService userService, UserManager<User> userManager, CancellationToken cancellationToken = default)
        {
            if (!await userService.AnyAsync(cancellationToken).ConfigureAwait(false))
            {
                var user = new RegisterUserCommandModel() { Name = "Nikusha", Email = "nikaovashvili@gmail.com", Username = "nika12", Password = "Test4123$" };
                await userService.RegisterAsync(user, cancellationToken).ConfigureAwait(false);
                var adminUser = await userManager.FindByNameAsync(user.Username).ConfigureAwait(false);
                await userManager.AddToRolesAsync(adminUser, new List<string> { Roles.Moderator.ToString(), Roles.Admin.ToString() }).ConfigureAwait(false);
            }
        }
    }
}
