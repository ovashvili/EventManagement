using EventManagement.Persistence.Context;
using EventManagement.Worker.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<EventExpirationService>();
                services.AddHostedService<ReservationExpirationWorker>();
                services.AddDbContext<EventDbContext>(options =>
                    options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection")));
            })
            .Build();
        await host.RunAsync().ConfigureAwait(false);

    }
}
