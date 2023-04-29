using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Events.Commands.CreateEvent;
using EventManagement.Application.Events.Commands.UpdateEvent;

namespace EventManagement.Application.Contracts
{
    public interface IEventService
	{
        Task<CommandResponse<EventDto>> CreateAsync(CreateEventCommandModel model, CancellationToken cancellationToken);
        Task<CommandResponse<EventDto>> GetByIdAsync(string id, CancellationToken cancellationToken);
        Task<CommandResponse<IEnumerable<EventDto>>> GetSubmittedEventsAsync(CancellationToken cancellationToken);
        Task<CommandResponse<EventDto>> UpdateAsync(string id, UpdateEventCommandModel model, CancellationToken cancellationToken);
        Task<CommandResponse<string>> PurchaseTicketAsync(string id, CancellationToken cancellationToken);
        Task<CommandResponse<string>> ReserveTicketAsync(string id, CancellationToken cancellationToken);
        Task<CommandResponse<IEnumerable<EventDto>>> GetActiveEventListAsync(CancellationToken cancellationToken);
        Task<CommandResponse<IEnumerable<EventDto>>> GetArchivedEventListAsync(CancellationToken cancellationToken);
        Task<CommandResponse<string>> MarkAsArchivedAsync(string id, CancellationToken cancellationToken);
        Task<CommandResponse<string>> MarkAsActiveAsync(string id, CancellationToken cancellationToken);

    }
}
