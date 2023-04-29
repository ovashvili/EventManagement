using EventManagement.API.Common;
using EventManagement.API.SwaggerSettings.Examples.RoleExamples;
using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Role.Commands.AddRoleToUser;
using EventManagement.Application.Role.Commands.RemoveRoleFromUser;
using EventManagement.Application.Role.Queries.GetUserRoles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace EventManagement.API.Controllers.v1
{
    [Route("/event")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class RoleController : ApiController
    {
        public RoleController(IMediator mediator) : base(mediator)
        {
        }
        /// <summary>
        /// Add role to user
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Should return command status</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Some unhandled server error occurred</response>
        [HttpPost("add-role-to-User")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CommandResponse<string>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, examplesProviderType: typeof(AddRoleToUserCommandModelExample))]
        public async Task<ActionResult<CommandResponse<string>>> AddRoleToUserAsync(string userId, string roleName, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new AddRoleToUserCommand { UserId = userId, RoleName = roleName }, cancellationToken).ConfigureAwait(false));
        }

        /// <summary>
        /// Remove role from user
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Should return command status</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Some unhandled server error occurred</response>
        [HttpDelete("{roleName}/{userId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CommandResponse<string>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, examplesProviderType: typeof(RemoveRoleFromUserQuery))]
        public async Task<ActionResult<CommandResponse<string>>> RemoveRoleFromUserAsync(string roleName, string userId, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new RemoveRoleFromUserQuery { RoleName = roleName, UserId = userId }, cancellationToken).ConfigureAwait(false));
        }

        /// <summary>
        /// Get user's roles
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Should return user's roles</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Some unhandled server error occurred</response>
        [HttpGet("get-user-roles")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CommandResponse<IEnumerable<RoleDto>>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, examplesProviderType: typeof(GetUserRolesQueryResponseExample))]
        public async Task<ActionResult<CommandResponse<IEnumerable<RoleDto>>>> GetUserRolesAsync(string userId, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetUserRolesQuery { UserId = userId }, cancellationToken).ConfigureAwait(false));
        }
    }
}
