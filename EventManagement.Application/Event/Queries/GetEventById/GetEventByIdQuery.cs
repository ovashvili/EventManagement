using EventManagement.Application.Commmon.Models;
using MediatR;

namespace EventManagement.Application.Events.Queries.GetEventDetails
{
    public class GetEventByIdQuery : IRequest<CommandResponse<EventDto>>
    {
        public string Id { get; set; }
    }
}
