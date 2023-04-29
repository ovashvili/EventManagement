using EventManagement.Application.Commmon.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Role.Commands.RemoveRoleFromUser
{
    public class RemoveRoleFromUserQuery : IRequest<CommandResponse<string>>
    {
        public string RoleName { get; set; }
        public string UserId { get; set; }
    }
}
