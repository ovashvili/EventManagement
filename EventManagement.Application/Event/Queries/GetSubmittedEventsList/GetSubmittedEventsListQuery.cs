using EventManagement.Application.Commmon.Models;
using MediatR;

namespace EventManagement.Application.Event.Queries.GetSubmittedEventsList
{
    public class GetSubmittedEventsListQuery : IRequest<CommandResponse<IEnumerable<EventDto>>>
    {

    }
}
