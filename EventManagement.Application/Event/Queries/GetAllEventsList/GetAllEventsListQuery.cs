using EventManagement.Application.Commmon.Models;
using MediatR;

namespace EventManagement.Application.Event.Queries.GetAllEventsList
{
    public class GetAllEventsListQuery : IRequest<CommandResponse<IEnumerable<EventDto>>>
    {

    }
}
