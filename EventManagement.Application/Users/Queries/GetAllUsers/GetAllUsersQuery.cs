using EventManagement.Application.Commmon.Models;
using MediatR;

namespace EventManagement.Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<CommandResponse<IEnumerable<UserDto>>>
    {
    }
}
