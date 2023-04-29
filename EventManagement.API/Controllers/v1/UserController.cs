using EventManagement.API.Common;
using EventManagement.API.SwaggerSettings.Examples.UserExamples;
using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Users.Commands.AuthenticateUser;
using EventManagement.Application.Users.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace EventManagement.API.Controllers.v1
{
    [Route("/user")]
    [ApiVersion("1.0")]
    [ApiController]
    public class UserController : ApiController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }
        /// <summary>
        /// Register user
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Should return command status</response>
        /// <response code="500">Some unhandled server error occurred</response>
        [ProducesResponseType(typeof(CommandResponse<string>), StatusCodes.Status200OK)]
        [HttpPost("register")]
        public async Task<ActionResult<CommandResponse<string>>> Register([FromBody] RegisterUserCommandModel model, CancellationToken cancellationToken)
        {
            return await Mediator.Send(new RegisterUserCommand { Model = model }, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Should return authenticated user</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Some unhandled server error occurred</response>
        [HttpPost("logIn")]
        [ProducesResponseType(typeof(CommandResponse<AuthenticateUserResponse>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, examplesProviderType: typeof(AuthenticateUserResponseExample))]
        public async Task<ActionResult<CommandResponse<AuthenticateUserResponse>>> AuthenticateUserAsync([FromBody] AuthenticateUserCommandModel model, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new AuthenticateUserCommand { Model = model }, cancellationToken));
        }
    }
}
