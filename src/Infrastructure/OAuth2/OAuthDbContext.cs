using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.OAuth2;

public class OAuthDbContext<TIdentityUser> : IdentityDbContext<TIdentityUser> where TIdentityUser : IdentityUser
{
	public OAuthDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

	public DbSet<Client> Clients => Set<Client>();

	public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
}
