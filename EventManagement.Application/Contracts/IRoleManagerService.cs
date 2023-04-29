using EventManagement.Application.Commmon.Models;

namespace EventManagement.Application.Contracts
{
    public interface IRoleManagerService
	{
        Task<CommandResponse<IEnumerable<string>>> GetUserRolesAsync(string userId, CancellationToken cancellationToken);
        Task<CommandResponse<string>> AddRoleToUserAsync(string userId, string roleName, CancellationToken cancellationToken);
        Task<CommandResponse<string>> RemoveRoleFromUserAsync(string userId, string roleName, CancellationToken cancellationToken);
    }
}
