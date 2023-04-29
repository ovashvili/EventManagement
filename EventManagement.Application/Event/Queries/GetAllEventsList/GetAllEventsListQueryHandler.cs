using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Contracts;
using MediatR;

namespace EventManagement.Application.Event.Queries.GetAllEventsList
{
    public class GetAllEventsListQueryHandler : IRequestHandler<GetAllEventsListQuery, CommandResponse<IEnumerable<EventDto>>>
    {
        private readonly IEventService _eventservice;

        public GetAllEventsListQueryHandler(IEventService eventservice)
        {
            _eventservice = eventservice;
        }

        public async Task<CommandResponse<IEnumerable<EventDto>>> Handle(GetAllEventsListQuery request, CancellationToken cancellationToken)
        {
            return await _eventservice.GetActiveEventListAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
