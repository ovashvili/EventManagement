using EventManagement.Application.Events.Commands.ReserveTicket;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.Examples;

namespace EventManagement.API.SwaggerSettings.Examples.EventExamples
{
    public class ReserveTicketCommandModelExample : IExamplesProvider<ReserveTicketCommandModel>
    {
        public ReserveTicketCommandModel GetExamples()
        {
            return new ReserveTicketCommandModel
            {
                EventId = "cb975a17-fded-45fc-9ffe-4994d35520a1"
            };
        }
    }
}
