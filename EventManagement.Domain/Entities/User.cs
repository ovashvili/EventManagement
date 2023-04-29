using Microsoft.AspNetCore.Identity;

namespace EventManagement.Domain.Entities
{
	public class User : IdentityUser
	{
		public string Name { get; set; }
        public List<Event> Events { get; set; }

    }
}
