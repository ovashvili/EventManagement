using EventManagement.API.Common;
using EventManagement.API.SwaggerSettings.Examples.EventExamples;
using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Event.Commands.MarkAsActive;
using EventManagement.Application.Event.Commands.MarkAsArchived;
using EventManagement.Application.Event.Queries.GetAllEventsList;
using EventManagement.Application.Event.Queries.GetSubmittedEventsList;
using EventManagement.Application.Events.Commands.CreateEvent;
using EventManagement.Application.Events.Commands.PurchaseTicket;
using EventManagement.Application.Events.Commands.ReserveTicket;
using EventManagement.Application.Events.Commands.UpdateEvent;
using EventManagement.Application.Events.Queries.GetActiveEventsList;
using EventManagement.Application.Events.Queries.GetArchivedEventsList;
using EventManagement.Application.Events.Queries.GetEventDetails;
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
    public class EventController : ApiController
    {
        public EventController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Create a event
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Should return command status</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Some unhandled server error occurred</response>
        [HttpPost("create")]
        [Authorize(Roles = "Basic, Moderator, Admin")]
        [ProducesResponseType(typeof(CommandResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<CommandResponse<string>>> CreateEventAsync([FromForm] CreateEventCommandModel model, CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(new CreateEventCommand { Model = model }, cancellationToken).ConfigureAwait(false));
        }

        /// <summary>
        /// Updates a event
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Should return command status</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Some unhandled server error occurred</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CommandResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<CommandResponse<string>>> UpdateEventAsync(string id, [FromForm] UpdateEventCommandModel model, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new UpdateEventCommand { Id = id, Model = model }, cancellationToken).ConfigureAwait(false));
        }

        /// <summary>
        /// Reserve a ticket
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Should return command status</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Some unhandled server error occurred</response>
        [HttpPost("reserve-a-ticket")]
        [ProducesResponseType(typeof(CommandResponse<string>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, examplesProviderType: typeof(ReserveTicketCommandModelExample))]
        public async Task<ActionResult<CommandResponse<string>>> ReserveTicketAsync(ReserveTicketCommandModel model, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new ReserveTicketCommand { Model = model }, cancellationToken).ConfigureAwait(false));
        }

        /// <summary>
        /// Purchase a ticket
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Should return command status</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Some unhandled server error occurred</response>
        [HttpPost("purchase-a-ticket")]
        [ProducesResponseType(typeof(CommandResponse<string>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, examplesProviderType: typeof(PurchaseTicketCommandModelExample))]
        public async Task<ActionResult<CommandResponse<string>>> PurchaseTicketAsync(PurchaseTicketCommandModel model, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new PurchaseTicketCommand { Model = model }, cancellationToken).ConfigureAwait(false));
        }

        /// <summary>
        /// Mark event as active
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Should return command status</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Some unhandled server error occurred</response>
        [HttpPost("mark-event-as-active")]
        [Authorize(Roles = "Admin, Moderator")]
        [ProducesResponseType(typeof(CommandResponse<string>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, examplesProviderType: typeof(MarkEventAsActiveCommandModelExample))]
        public async Task<ActionResult<CommandResponse<string>>> MarkEventAsActive(MarkEventAsActiveCommandModel model, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new MarkEventAsActiveCommand { Model = model }, cancellationToken).ConfigureAwait(false));
        }

        /// <summary>
        /// Mark event as Archived
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Should return command status</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Some unhandled server error occurred</response>
        [HttpPost("mark-event-as-archived")]
        [Authorize(Roles = "Admin, Moderator")]
        [ProducesResponseType(typeof(CommandResponse<string>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, examplesProviderType: typeof(MarkEventAsActiveCommandModelExample))]
        public async Task<ActionResult<CommandResponse<string>>> MarkEventAsArchived(MarkEventAsArchivedCommandModel model, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new MarkEventAsArchivedCommand { Model = model }, cancellationToken).ConfigureAwait(false));
        }

        /// <summary>
        /// Get event details
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Should return event</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Some unhandled server error occurred</response>
        [HttpGet("get-details-{id}")]
        [ProducesResponseType(typeof(CommandResponse<EventDto>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, examplesProviderType: typeof(GetEventDetailsExample))]
        public async Task<ActionResult<CommandResponse<EventDto>>> GetEventDetailsAsync(string id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetEventByIdQuery { Id = id }, cancellationToken).ConfigureAwait(false));
        }

        /// <summary>
        /// Get active event list
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Should return a list of events</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Some unhandled server error occurred</response>
        [HttpGet("get-all-active-events")]
        [ProducesResponseType(typeof(CommandResponse<IEnumerable<EventDto>>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, examplesProviderType: typeof(GetActiveEventListExample))]
        public async Task<ActionResult<CommandResponse<IEnumerable<EventDto>>>> GetActiveEventsAsync(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetActiveEventsListQuery { }, cancellationToken).ConfigureAwait(false));
        }

        /// <summary>
        /// Get archived event list
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Should return list of events</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Some unhandled server error occurred</response>
        [HttpGet("get-all-archived-events")]
        [Authorize(Roles = "Admin, Moderator")]
        [ProducesResponseType(typeof(CommandResponse<IEnumerable<EventDto>>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, examplesProviderType: typeof(GetArchivedEventListExample))]
        public async Task<ActionResult<CommandResponse<IEnumerable<EventDto>>>> GetArchivedEventsAsync(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetArchivedEventsListQuery { }, cancellationToken).ConfigureAwait(false));
        }
        /// <summary>
        /// Get submitted event list
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Should return a list of events</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Some unhandled server error occurred</response>
        [HttpGet("get-all-submitted-events")]
        [ProducesResponseType(typeof(CommandResponse<IEnumerable<EventDto>>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, examplesProviderType: typeof(GetSubmittedEventListExample))]
        public async Task<ActionResult<CommandResponse<IEnumerable<EventDto>>>> GetSubmittedEventsAsync(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetSubmittedEventsListQuery { }, cancellationToken).ConfigureAwait(false));
        }

        [HttpGet("get-all-events")]
        [ProducesResponseType(typeof(CommandResponse<IEnumerable<EventDto>>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, examplesProviderType: typeof(GetSubmittedEventListExample))]
        public async Task<ActionResult<CommandResponse<IEnumerable<EventDto>>>> GetActionResultAsync(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetAllEventsListQuery { }, cancellationToken).ConfigureAwait(false));
        }
    }
}
