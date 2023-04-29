using EventManagement.Application.Commmon.Models;
using Swashbuckle.AspNetCore.Filters;

namespace EventManagement.API.SwaggerSettings.Examples.EventExamples
{
    public class GetEventDetailsExample : IExamplesProvider<EventDto>
    {
        public EventDto GetExamples()
        {
            return new EventDto
            {
                Id = "cb975a17-fded-45fc-9ffe-4994d35520a1",
                Name = "John's Event",
                Address = "John's Event Address",
                Description = "John's Event's Description",
                TicketQuantity = 100,
                AvailableTickets = 100,
                StartDate = DateTime.Now.AddDays(10),
                EndDate = DateTime.Now.AddDays(10).AddHours(5),
                PhotoPath = "7d934034c08e468cac68c7b5d71134a3-IMG_20210603_121741.jpg",
                IsActive = true,
                IsArchived = false,
                ModifiableTill = DateTime.Now.AddDays(5)
            };
        }
    }
}
