using EventManagement.Application.Commmon.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Role.Commands.AddRoleToUser
{
    public class AddRoleToUserCommand : IRequest<CommandResponse<string>>
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
    }
}
