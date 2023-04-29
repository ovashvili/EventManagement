using EventManagement.Application.Commmon.Models;
using MediatR;

namespace EventManagement.Application.Users.Commands.AuthenticateUser
{
    public class AuthenticateUserCommand : IRequest<CommandResponse<AuthenticateUserResponse>>
    {
        public AuthenticateUserCommandModel Model { get; set; }
    }

    public class AuthenticateUserResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }

    public class AuthenticateUserCommandModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }


}
