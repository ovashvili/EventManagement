using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Contracts;
using EventManagement.Application.Events.Queries.GetArchivedEventsList;
using MediatR;

namespace EventManagement.Application.Event.Queries.GetSubmittedEventsList
{
    public class GetArchivedEventsListQueryHandler : IRequestHandler<GetSubmittedEventsListQuery, CommandResponse<IEnumerable<EventDto>>>
    {
        private readonly IEventService _eventservice;

        public GetArchivedEventsListQueryHandler(IEventService eventservice)
        {
            _eventservice = eventservice;
        }

        public async Task<CommandResponse<IEnumerable<EventDto>>> Handle(GetSubmittedEventsListQuery request, CancellationToken cancellationToken)
        {
            return await _eventservice.GetSubmittedEventsAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
