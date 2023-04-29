using EventManagement.Application.Commmon.Models;
using MediatR;

namespace EventManagement.Application.Events.Queries.GetArchivedEventsList
{
    public class GetArchivedEventsListQuery : IRequest<CommandResponse<IEnumerable<EventDto>>>
    {

    }
}
