using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Contracts;
using MediatR;

namespace EventManagement.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, CommandResponse<UserDto>>
    {
        private readonly IUserService _userService;

        public GetUserByIdQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CommandResponse<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetByIdAsync(request.Id, cancellationToken).ConfigureAwait(false);
        }
    }
}
