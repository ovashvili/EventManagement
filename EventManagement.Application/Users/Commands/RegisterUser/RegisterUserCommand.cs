using EventManagement.Application.Commmon.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<CommandResponse<string>>
    {
        public RegisterUserCommandModel Model { get; set; }
    }

    public class RegisterUserCommandModel
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
