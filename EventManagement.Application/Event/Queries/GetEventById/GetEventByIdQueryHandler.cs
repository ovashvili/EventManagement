using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Events.Queries.GetEventDetails
{
    public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, CommandResponse<EventDto>>
    {
        private readonly IEventService _eventService;

        public GetEventByIdQueryHandler(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<CommandResponse<EventDto>> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            return await _eventService.GetByIdAsync(request.Id, cancellationToken).ConfigureAwait(false);
        }
    }
}
