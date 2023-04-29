using AutoMapper;
using EventManagement.Application.Commmon.Enums;
using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Contracts;
using EventManagement.Domain.Entities;
using EventManagement.Persistence.Context;
using EventManagement.Shared.Localizations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Infrastructure.Services
{
    public class RoleManagerService : IRoleManagerService
	{
        private readonly EventDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public RoleManagerService(EventDbContext db, UserManager<User> userManager, IMapper mapper)
        {
            _context = db;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<CommandResponse<string>> AddAsync(string roleName, CancellationToken cancellationToken)
        {
            var roleStore = new RoleStore<IdentityRole>(_context);

            if (await _context.Roles.AnyAsync(x => x.Name == roleName, cancellationToken).ConfigureAwait(false))
                return new CommandResponse<string>(StatusCode.Conflict, "Role '" + roleName + "' does already exist");

            await _context.Roles.AddAsync(new IdentityRole(roleName.Trim()), cancellationToken).ConfigureAwait(false);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return new CommandResponse<string>(StatusCode.Success);
        }

        public async Task<CommandResponse<string>> AddRoleToUserAsync(string userId, string roleName, CancellationToken cancellationToken)
        {
            if (!await _context.Roles.AnyAsync(x => x.Name == roleName, cancellationToken).ConfigureAwait(false))
                return new CommandResponse<string>(StatusCode.NotFound, "Role '" + roleName + "' does not exist");

            var user = await _context.Users.FindAsync(userId).ConfigureAwait(false);

            if (user == null)
                return new CommandResponse<string>(StatusCode.NotFound, ErrorMessages.UserNotFound);

            var mappedUser = _mapper.Map<User>(user);

            await _userManager.AddToRoleAsync(mappedUser, roleName).ConfigureAwait(false);

            return new CommandResponse<string>(StatusCode.Success);
        }

        public async Task<CommandResponse<IEnumerable<string>>> GetUserRolesAsync(string userId, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(userId).ConfigureAwait(false);

            if (user == null)
                return new CommandResponse<IEnumerable<string>>(StatusCode.NotFound, ErrorMessages.UserNotFound);

            var userRoles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            if (!userRoles.Any())
                return new CommandResponse<IEnumerable<string>>(StatusCode.NotFound, ErrorMessages.UserHasNoRoles);

            return new CommandResponse<IEnumerable<string>>(StatusCode.Success, null, userRoles);
        }

        public async Task<CommandResponse<string>> RemoveRoleFromUserAsync(string userId, string roleName, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(userId).ConfigureAwait(false);

            if (user == null)
                return new CommandResponse<string>(StatusCode.NotFound, ErrorMessages.UserHasNoRoles);

            if (!_context.Roles.Any(x => x.Name == roleName))
                return new CommandResponse<string>(StatusCode.NotFound, "Role '" + roleName + "' does not exist");

            var mappedUser = _mapper.Map<User>(user);

            await _userManager.RemoveFromRoleAsync(mappedUser, roleName).ConfigureAwait(false);

            return new CommandResponse<string>(StatusCode.Success);
        }
    }
}

