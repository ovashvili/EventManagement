using EventManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventManagement.Persistence.Configurations
{
	public class EventConfiguration : IEntityTypeConfiguration<Event>
	{
		public void Configure(EntityTypeBuilder<Event> builder)
		{
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(85);

            builder.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(95);
                
            builder.Property(e => e.Description)
                .HasMaxLength(500);

            builder.Property(e => e.TicketQuantity)
                .IsRequired();

            builder.Property(e => e.AvailableTickets)
                .IsRequired();

            builder.Property(e => e.StartDate)
                .IsRequired();

            builder.Property(e => e.EndDate)
                .IsRequired();

            builder.Property(e => e.PhotoPath)
                .HasMaxLength(200);

            builder.Property(e => e.IsActive)
                .IsRequired();

            builder.Property(e => e.ModifiableTill)
                .IsRequired();

            builder.HasOne(e => e.User)
                .WithMany(u => u.Events)
                .HasForeignKey(e => e.UserID)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
