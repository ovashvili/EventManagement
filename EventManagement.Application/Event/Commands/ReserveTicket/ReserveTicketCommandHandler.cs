using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Contracts;
using MediatR;

namespace EventManagement.Application.Events.Commands.ReserveTicket
{
    public class ReserveTicketCommandHandler : IRequestHandler<ReserveTicketCommand, CommandResponse<string>>
    {
        private readonly IEventService _eventService;

        public ReserveTicketCommandHandler(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<CommandResponse<string>> Handle(ReserveTicketCommand request, CancellationToken cancellationToken)
        {
            return await _eventService.ReserveTicketAsync(request.Model.EventId, cancellationToken).ConfigureAwait(false);
        }
    }
}
