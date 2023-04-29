using EventManagement.Application.Events.Commands.PurchaseTicket;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.Examples;

namespace EventManagement.API.SwaggerSettings.Examples.EventExamples
{
    public class PurchaseTicketCommandModelExample : IExamplesProvider<PurchaseTicketCommandModel>
    {
        public PurchaseTicketCommandModel GetExamples()
        {
            return new PurchaseTicketCommandModel
            {
                EventId = "cb975a17-fded-45fc-9ffe-4994d35520a1"
            };
        }
    }
}
