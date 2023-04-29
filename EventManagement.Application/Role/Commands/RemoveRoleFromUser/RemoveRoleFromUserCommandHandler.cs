using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Role.Commands.RemoveRoleFromUser
{
    public class RemoveRoleFromUserQueryHandler : IRequestHandler<RemoveRoleFromUserQuery, CommandResponse<string>>
    {
        private readonly IRoleManagerService _roleManagerService;

        public RemoveRoleFromUserQueryHandler(IRoleManagerService roleManagerService)
        {
            _roleManagerService = roleManagerService;
        }

        public async Task<CommandResponse<string>> Handle(RemoveRoleFromUserQuery request, CancellationToken cancellationToken)
        {
            return await _roleManagerService.RemoveRoleFromUserAsync(request.UserId, request.RoleName, cancellationToken).ConfigureAwait(false);
        }
    }
}
