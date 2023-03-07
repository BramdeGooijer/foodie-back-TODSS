using Microsoft.EntityFrameworkCore;

namespace Template.Infrastructure.OAuth2;

internal static class OAuth2ModelBuilderExtensions
{
	public static ModelBuilder ConfigureOAuth2(this ModelBuilder builder)
	{
		builder.Entity<RefreshToken>()
			.HasKey(refreshToken => refreshToken.Token);

		builder.Entity<RefreshToken>()
			.Property(refreshToken => refreshToken.Token)
			.HasMaxLength(44);

		return builder;
	}
}
