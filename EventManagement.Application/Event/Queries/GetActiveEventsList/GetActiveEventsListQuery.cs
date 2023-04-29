using EventManagement.Application.Commmon.Models;
using MediatR;

namespace EventManagement.Application.Events.Queries.GetActiveEventsList
{
    public class GetActiveEventsListQuery : IRequest<CommandResponse<IEnumerable<EventDto>>>
    {
    }
}
