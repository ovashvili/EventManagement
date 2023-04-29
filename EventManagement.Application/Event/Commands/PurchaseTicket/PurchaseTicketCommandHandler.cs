using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Events.Commands.PurchaseTicket
{
    public class PurchaseTicketCommandHandler : IRequestHandler<PurchaseTicketCommand, CommandResponse<string>>
    {
        private readonly IEventService _eventService;
        public PurchaseTicketCommandHandler(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<CommandResponse<string>> Handle(PurchaseTicketCommand request, CancellationToken cancellationToken)
        {
            return await _eventService.PurchaseTicketAsync(request.Model.EventId, cancellationToken).ConfigureAwait(false);
        }
    }
}
