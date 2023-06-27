using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Events.Commands.CreateEvent;
using EventManagement.Application.Events.Commands.UpdateEvent;
using EventManagement.MVC.Models;
using Mapster;

namespace EventManagement.MVC.Infrastructure.Mapping
{
    public static class MappingConfig
    {
        public static void RegisterMaps(this IServiceCollection services)
        {
            TypeAdapterConfig<CreateEventViewModel, CreateEventCommandModel>.NewConfig().Ignore(x => x.Photo).TwoWays();
            TypeAdapterConfig<UpdateEventViewModel, UpdateEventCommandModel>.NewConfig().Ignore(x => x.Photo).TwoWays();
            TypeAdapterConfig<EventDto, EventViewModel>.NewConfig().TwoWays();

        }
    }
}
