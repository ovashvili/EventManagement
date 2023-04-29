using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Contracts;
using MediatR;

namespace EventManagement.Application.Events.Queries.GetArchivedEventsList
{
    public class GetArchivedEventsListQueryHandler : IRequestHandler<GetArchivedEventsListQuery, CommandResponse<IEnumerable<EventDto>>>
    {
        private readonly IEventService _eventservice;

        public GetArchivedEventsListQueryHandler(IEventService eventservice)
        {
            _eventservice = eventservice;
        }

        public async Task<CommandResponse<IEnumerable<EventDto>>> Handle(GetArchivedEventsListQuery request, CancellationToken cancellationToken)
        {
            return await _eventservice.GetArchivedEventListAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
