using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Contracts;
using MediatR;

namespace EventManagement.Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, CommandResponse<IEnumerable<UserDto>>>
    {
        private readonly IUserService _userService;
        public GetAllUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<CommandResponse<IEnumerable<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetAllUsersAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
