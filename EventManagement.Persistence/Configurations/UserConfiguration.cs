using EventManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventManagement.Persistence.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.Property(x => x.Name)
				.IsUnicode(false)
				.IsRequired()
				.HasMaxLength(80);

			builder.HasMany(e => e.Events)
				.WithOne(p => p.User);
		}
	}
}
