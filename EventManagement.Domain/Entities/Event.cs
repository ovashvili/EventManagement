﻿namespace EventManagement.Domain.Entities
{
	public class Event
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string City { get; set; }
		public string Address { get; set; }
		public string Description { get; set; }
		public int TicketQuantity { get; set; }
		public int AvailableTickets { get; set; }
		public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PhotoPath { get; set; }
		public string UserID { get; set; }
		public User User { get; set; }
		public bool IsActive { get; set; }
		public bool IsArchived { get; set; }
		public DateTime ModifiableTill { get; set; }

    }
}
