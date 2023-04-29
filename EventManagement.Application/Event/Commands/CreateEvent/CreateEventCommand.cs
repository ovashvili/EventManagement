using EventManagement.Application.Commmon.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EventManagement.Application.Events.Commands.CreateEvent
{
    public class CreateEventCommand : IRequest<CommandResponse<EventDto>>
    {
        public CreateEventCommandModel Model { get; set; }
    }

    public class CreateEventCommandModel
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int TicketQuantity { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IFormFile Photo { get; set; }
    }
}
