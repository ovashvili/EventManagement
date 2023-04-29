using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Role.Queries.GetUserRoles
{
    public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, CommandResponse<IEnumerable<string>>>
    {
        private readonly IRoleManagerService _roleManagerService;

        public GetUserRolesQueryHandler(IRoleManagerService roleManagerService)
        {
            _roleManagerService = roleManagerService;
        }

        public async Task<CommandResponse<IEnumerable<string>>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            return await _roleManagerService.GetUserRolesAsync(request.UserId, cancellationToken).ConfigureAwait(false);
        }
    }
}
