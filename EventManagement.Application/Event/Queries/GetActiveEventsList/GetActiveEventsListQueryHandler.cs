using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Events.Queries.GetActiveEventsList
{
    public class GetActiveEventsListQueryHandler : IRequestHandler<GetActiveEventsListQuery, CommandResponse<IEnumerable<EventDto>>>
    {
        private readonly IEventService _eventService;

        public GetActiveEventsListQueryHandler(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<CommandResponse<IEnumerable<EventDto>>> Handle(GetActiveEventsListQuery request, CancellationToken cancellationToken)
        {
            return await _eventService.GetActiveEventListAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
