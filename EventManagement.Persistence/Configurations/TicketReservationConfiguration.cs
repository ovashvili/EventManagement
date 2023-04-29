using EventManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Persistence.Configurations
{
    public class TicketReservationsConfiguration : IEntityTypeConfiguration<TicketReservations>
    {
        public void Configure(EntityTypeBuilder<TicketReservations> builder)
        {
            builder.HasKey(tr => tr.Id);

            builder.Property(tr => tr.UserId)
                .IsRequired();

            builder.Property(tr => tr.EventId)
                .IsRequired();

            builder.Property(tr => tr.ReservationTime)
                .IsRequired();

        }
    }
}
