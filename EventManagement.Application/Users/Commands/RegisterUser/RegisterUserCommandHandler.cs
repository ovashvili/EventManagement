using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Contracts;
using MediatR;

namespace EventManagement.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, CommandResponse<string>>
    {
        private readonly IUserService _userService;

        public RegisterUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CommandResponse<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.RegisterAsync(request.Model, cancellationToken).ConfigureAwait(false);
        }
    }
}
