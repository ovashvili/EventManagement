using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Domain.Entities
{
    public class TicketReservations
    {
        public string Id { get; set; }
        public string EventId { get; set; }
        public Event Event { get; set; }
        public string UserId { get; set; }
        public bool IsReserved { get; set; }
        public bool IsBought { get; set; }
        public DateTime ReservationTime { get; set; }
    }
}
