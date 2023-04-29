using EventManagement.Application.Commmon.Models;
using MediatR;

namespace EventManagement.Application.Event.Commands.MarkAsActive
{
    public class MarkEventAsActiveCommand : IRequest<CommandResponse<string>>
    {
        public MarkEventAsActiveCommandModel Model { get; set; }
    }
    public class MarkEventAsActiveCommandModel
    {
        public string EventId { get; set; }
    }
}
