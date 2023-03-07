using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Domain.Entities;
using Template.Infrastructure.Identity;

namespace Template.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder
			.HasOne<IdentityUser>()
			.WithOne()
			.HasForeignKey<User>(nameof(User.IdentityId));
	}
}
