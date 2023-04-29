using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.API.Common
{
    [ApiController]
    [Produces("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ApiController : ControllerBase
    {
        protected readonly IMediator Mediator;
        public ApiController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
