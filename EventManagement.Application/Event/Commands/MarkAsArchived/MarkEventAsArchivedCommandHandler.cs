using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Contracts;
using MediatR;

namespace EventManagement.Application.Event.Commands.MarkAsArchived
{
    public class MarkEventAsArchivedCommandHandler : IRequestHandler<MarkEventAsArchivedCommand, CommandResponse<string>>
    {
        private readonly IEventService _eventService;

        public MarkEventAsArchivedCommandHandler(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<CommandResponse<string>> Handle(MarkEventAsArchivedCommand request, CancellationToken cancellationToken)
        {
            return await _eventService.MarkAsArchivedAsync(request.Model.EventId, cancellationToken).ConfigureAwait(false);
        }
    }
}
