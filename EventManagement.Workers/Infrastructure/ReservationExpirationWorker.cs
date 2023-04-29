using EventManagement.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Worker.Infrastructure
{
    public class ReservationExpirationWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ReservationExpirationWorker(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<EventDbContext>();

                    var reservations = await dbContext.TicketReservations.ToListAsync(cancellationToken).ConfigureAwait(false);

                    foreach (var reservation in reservations)
                    {
                        if (DateTime.UtcNow >= reservation.ReservationTime && reservation.IsReserved)
                        {
                            reservation.IsReserved = false;

                            var eventEntity = dbContext.Events.FirstOrDefault(e => e.Id == reservation.EventId);
                            if (eventEntity != null)
                            {
                                eventEntity.AvailableTickets++;
                            }

                            await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                        }
                    }
                }
                await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken).ConfigureAwait(false);
            }
        }
    }
}

