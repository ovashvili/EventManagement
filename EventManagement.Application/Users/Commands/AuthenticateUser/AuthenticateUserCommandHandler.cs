using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Users.Commands.AuthenticateUser
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, CommandResponse<AuthenticateUserResponse>>
    {
        private readonly IUserService _userService;

        public AuthenticateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task<CommandResponse<AuthenticateUserResponse>> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            return _userService.AuthenticateAsync(request.Model);
        }
    }
}
