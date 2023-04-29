using AutoMapper;
using EventManagement.Application.Commmon.Enums;
using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Contracts;
using EventManagement.Application.Users.Commands.AuthenticateUser;
using EventManagement.Application.Users.Commands.RegisterUser;
using EventManagement.Domain;
using EventManagement.Domain.Entities;
using EventManagement.Infrastructure.Helpers;
using EventManagement.Infrastructure.Helpers.JWT;
using EventManagement.Persistence.Context;
using EventManagement.Shared.Localizations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EventManagement.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly EventDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IOptions<JWTConfiguration> _jwtAuthOptions;
        private readonly IMapper _mapper;
        public UserService(EventDbContext db,
                           IMapper mapper,
                           UserManager<User> userManager,
                           SignInManager<User> signInManager,
                           IOptions<JWTConfiguration> jwtAuthOptions)
        {
            _context = db;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtAuthOptions = jwtAuthOptions;
        }
        public async Task<bool> AnyAsync(CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<CommandResponse<AuthenticateUserResponse>> AuthenticateAsync(AuthenticateUserCommandModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email).ConfigureAwait(false);

            if (user == null)
            {
                return new CommandResponse<AuthenticateUserResponse>(StatusCode.BadRequest, ErrorMessages.IncorrectCredentials);
            }

            var userRoles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, lockoutOnFailure: false);

            var token = ServiceHelper.GenerateSecurityToken(model.Email, user.Id, userRoles.ToList(), _jwtAuthOptions);

            var authenticateUser = _mapper.Map<AuthenticateUserResponse>(user);

            authenticateUser.Token = token;

            if (result.Succeeded)
            {
                return new CommandResponse<AuthenticateUserResponse>(StatusCode.Success, null, authenticateUser);
            }

            return new CommandResponse<AuthenticateUserResponse>(StatusCode.BadRequest, ErrorMessages.IncorrectCredentials);

        }

        public async Task<CommandResponse<UserDto>> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(id).ConfigureAwait(false);

            if (user == null)
                return new CommandResponse<UserDto>(StatusCode.NotFound, ErrorMessages.UserNotFound);

            var userDto = _mapper.Map<UserDto>(user);

            return new CommandResponse<UserDto>(StatusCode.Success, null, userDto);
        }

        public async Task<CommandResponse<string>> RegisterAsync(RegisterUserCommandModel model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                return new CommandResponse<string>(StatusCode.BadRequest, null, ErrorMessages.ArgumentNull);
            }
            var res = await _userManager.FindByEmailAsync(model.Email).ConfigureAwait(false);

            if (res != null)
            {
                return new CommandResponse<string>(StatusCode.Conflict, null, ErrorMessages.ExistingEmail);
            }

            var user = _mapper.Map<User>(model);

            var result = await _userManager.CreateAsync(user, model.Password).ConfigureAwait(false);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Basic.ToString()).ConfigureAwait(false);
                return new CommandResponse<string>(StatusCode.Success, null, result.ToString());
            }
            return new CommandResponse<string>(StatusCode.BadRequest, null, result.ToString());
        }
        public async Task<CommandResponse<IEnumerable<UserDto>>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            var users = await _context.Users.ToListAsync().ConfigureAwait(false);

            var res = _mapper.Map<IEnumerable<UserDto>>(users);
            foreach (var user in res)
            {
                user.Roles = await _userManager.GetRolesAsync(_mapper.Map<User>(user)).ConfigureAwait(false);
            }

            return new CommandResponse<IEnumerable<UserDto>>(StatusCode.Success, null, res);

        }
    }
}
