using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Contracts;
using MediatR;

namespace EventManagement.Application.Event.Commands.MarkAsActive
{
    public class MarkEventAsActiveCommandHandler : IRequestHandler<MarkEventAsActiveCommand, CommandResponse<string>>
    {
        private readonly IEventService _eventService;

        public MarkEventAsActiveCommandHandler(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<CommandResponse<string>> Handle(MarkEventAsActiveCommand request, CancellationToken cancellationToken)
        {
            return await _eventService.MarkAsActiveAsync(request.Model.EventId, cancellationToken).ConfigureAwait(false);
        }
    }
}
