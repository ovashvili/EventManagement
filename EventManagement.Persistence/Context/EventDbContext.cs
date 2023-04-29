using EventManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EventManagement.Persistence.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Emit;

namespace EventManagement.Persistence.Context
{
    public class EventDbContext : IdentityDbContext<User>
	{
		public EventDbContext(DbContextOptions<EventDbContext> options)
			: base(options)
		{
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<TicketReservations> TicketReservations { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(EventDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
