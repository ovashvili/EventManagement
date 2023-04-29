using EventManagement.Application.Event.Commands.MarkAsActive;
using EventManagement.Application.Event.Commands.MarkAsArchived;
using Swashbuckle.AspNetCore.Filters;

namespace EventManagement.API.SwaggerSettings.Examples.EventExamples
{
    public class MarkEventAsActiveCommandModelExample : IExamplesProvider<MarkEventAsActiveCommandModel>
    {
        public MarkEventAsActiveCommandModel GetExamples()
        {
            return new MarkEventAsActiveCommandModel
            {
                EventId = " cb975a17-fded-45fc-9ffe-4994d35520a1"
            };
        }
    }
}
