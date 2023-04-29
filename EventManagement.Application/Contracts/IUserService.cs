using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Users.Commands.AuthenticateUser;
using EventManagement.Application.Users.Commands.RegisterUser;

namespace EventManagement.Application.Contracts
{
    public interface IUserService
	{
        Task<CommandResponse<AuthenticateUserResponse>> AuthenticateAsync(AuthenticateUserCommandModel model);
        Task<CommandResponse<UserDto>> GetByIdAsync(string id, CancellationToken cancellationToken);
        Task<CommandResponse<string>> RegisterAsync(RegisterUserCommandModel model, CancellationToken cancellationToken);
        Task<CommandResponse<IEnumerable<UserDto>>> GetAllUsersAsync(CancellationToken cancellationToken);
        Task<bool> AnyAsync(CancellationToken cancellationToken);
    }
}
