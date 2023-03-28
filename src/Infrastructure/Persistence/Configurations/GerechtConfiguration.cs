using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Domain.Entities;

namespace Template.Infrastructure.Persistence.Configurations;

public class GerechtConfiguration : IEntityTypeConfiguration<Gerecht>
{
	public void Configure(EntityTypeBuilder<Gerecht> builder)
	{
		
	}
}
