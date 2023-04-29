using EventManagement.Application.Event.Commands.MarkAsArchived;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.Examples;

namespace EventManagement.API.SwaggerSettings.Examples.EventExamples
{
    public class MarkEventAsArchivedCommandModelExample : IExamplesProvider<MarkEventAsArchivedCommandModel>
    {
        public MarkEventAsArchivedCommandModel GetExamples()
        {
            return new MarkEventAsArchivedCommandModel
            {
                EventId = " cb975a17-fded-45fc-9ffe-4994d35520a1"
            };
        }
    }
}
