using EventManagement.Application.Commmon.Models;
using MediatR;

namespace EventManagement.Application.Event.Commands.MarkAsArchived
{
    public class MarkEventAsArchivedCommand : IRequest<CommandResponse<string>>
    {
        public MarkEventAsArchivedCommandModel Model { get; set; }
    }
    public class MarkEventAsArchivedCommandModel
    {
        public string EventId { get; set; }
    }
}
