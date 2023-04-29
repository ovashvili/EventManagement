using EventManagement.Persistence.Context;

namespace EventManagement.Worker.Infrastructure
{
    public class EventExpirationService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public EventExpirationService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<EventDbContext>();

                    // Filter the events that are active and have an event date less than or equal to the current time
                    var events = dbContext.Events.Where(e => e.IsActive && DateTime.UtcNow >= e.StartDate);

                    foreach (var eventItem in events)
                    {
                        eventItem.IsActive = false;
                        eventItem.IsArchived = true;
                    }

                    await dbContext.SaveChangesAsync(stoppingToken).ConfigureAwait(false);
                }

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken).ConfigureAwait(false);
            }
        }
    }
}
