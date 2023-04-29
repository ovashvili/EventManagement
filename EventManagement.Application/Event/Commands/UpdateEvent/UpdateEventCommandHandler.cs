using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Contracts;
using MediatR;

namespace EventManagement.Application.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, CommandResponse<EventDto>>
    {
        private readonly IEventService _eventService;

        public UpdateEventCommandHandler(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<CommandResponse<EventDto>> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            return await _eventService.UpdateAsync(request.Id, request.Model, cancellationToken).ConfigureAwait(false);
        }
    }
}
