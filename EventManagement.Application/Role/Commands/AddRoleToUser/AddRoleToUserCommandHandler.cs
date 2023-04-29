using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Role.Commands.AddRoleToUser
{
    public class AddRoleToUserCommandHandler : IRequestHandler<AddRoleToUserCommand, CommandResponse<string>>
    {
        private readonly IRoleManagerService _roleManagerService;

        public AddRoleToUserCommandHandler(IRoleManagerService roleManagerService)
        {
            _roleManagerService = roleManagerService;
        }

        public Task<CommandResponse<string>> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
        {
            return _roleManagerService.AddRoleToUserAsync(request.UserId, request.RoleName, cancellationToken);
        }
    }
}
