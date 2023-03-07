using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Application.Common.Interfaces;
using Template.Infrastructure.Files;
using Template.Infrastructure.Identity;
using Template.Infrastructure.OAuth2;
using Template.Infrastructure.Persistence;
using Template.Infrastructure.Persistence.Interceptors;
using Template.Infrastructure.Services;
using IdentityUser = Template.Infrastructure.Identity.IdentityUser;

namespace Template.Infrastructure;

public static class ConfigureServices
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddScoped<AuditableEntityInterceptor>();
		services.AddScoped<SoftDeletableEntityInterceptor>();

		services.AddDbContext<ApplicationDbContext>(options =>
			options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")!,
				builder =>
				{
					builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
					builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
				})
		);
		services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
		services.AddScoped<ApplicationDbContextInitialiser>();

		services.AddOptions<OAuth2Options>()
			.Bind(configuration.GetSection("Authentication").GetSection(OAuth2Options.OAuth2))
			.ValidateDataAnnotations()
			.ValidateOnStart();

		services.AddIdentity<IdentityUser, IdentityRole>()
			.AddEntityFrameworkStores<ApplicationDbContext>()
			.AddDefaultTokenProviders();
		services.Configure<IdentityOptions>(options =>
		{
			options.Password.RequireNonAlphanumeric = false;
			options.Password.RequiredLength = 8;
			options.SignIn.RequireConfirmedEmail = true;
			options.User.RequireUniqueEmail = true;
		});

		services.AddTransient<IdentityService>();
		services.AddTransient<IIdentityService, IdentityService>();
		services.AddTransient<ITokenService, TokenService>();
		services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();
		services.AddTransient<IDateTime, DateTimeService>();

		return services;
	}

	public static WebApplication InitialiseAndSeedDatabase(this WebApplication app)
	{
		var task = Task.Run(async () =>
		{
			using var scope = app.Services.CreateScope();
			var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
			await initialiser.InitialiseAsync();
			await initialiser.SeedAsync();
		});
		task.Wait();
		return app;
	}
}
