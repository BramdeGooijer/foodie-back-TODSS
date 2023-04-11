using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Template.Application.Common.Interfaces;
using Template.Domain.Entities;
using Template.Domain.Enums;
using Template.Infrastructure.Common;
using Template.Infrastructure.Identity;
using Template.Infrastructure.OAuth2;
using Template.Infrastructure.Persistence.Interceptors;

namespace Template.Infrastructure.Persistence;

public class ApplicationDbContext : OAuthDbContext<IdentityUser>, IApplicationDbContext
{
	private readonly IMediator _mediator;
	private readonly AuditableEntityInterceptor _auditableEntityInterceptor;

	public ApplicationDbContext(
		DbContextOptions<ApplicationDbContext> options,
		IMediator mediator,
		AuditableEntityInterceptor auditableEntityInterceptor) : base(options)
	{
		AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

		_mediator = mediator;
		_auditableEntityInterceptor = auditableEntityInterceptor;
	}

	public DbSet<Recipe> Recipes => Set<Recipe>();

	public DbSet<Allergy> Allergies => Set<Allergy>();
	
	public DbSet<Product> Products => Set<Product>();
	
	public DbSet<Ingredient> Ingredients => Set<Ingredient>();
	
	public DbSet<Requirement> Requirements => Set<Requirement>();
	
	public DbSet<CookingStep> CookingSteps => Set<CookingStep>();

	public DbSet<Season> Seasons => Set<Season>();

	public DbSet<TodoList> TodoLists => Set<TodoList>();

	public DbSet<TodoItem> TodoItems => Set<TodoItem>();

	public new DbSet<User> Users => Set<User>();

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.ConfigureIdentity();
		builder.ConfigureOAuth2();

		builder.AddEnumStringConversions();

		builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.AddInterceptors(_auditableEntityInterceptor);
	}

	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		await _mediator.DispatchDomainEvents(this);

		return await base.SaveChangesAsync(cancellationToken);
	}
}
