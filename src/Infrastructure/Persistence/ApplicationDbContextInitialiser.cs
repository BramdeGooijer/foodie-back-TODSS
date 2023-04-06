using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities;
using Template.Infrastructure.OAuth2;
using IdentityUser = Template.Infrastructure.Identity.IdentityUser;

namespace Template.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
	private readonly ILogger<ApplicationDbContextInitialiser> _logger;
	private readonly ApplicationDbContext _context;
	private readonly UserManager<IdentityUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;

	public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
	{
		_logger = logger;
		_context = context;
		_userManager = userManager;
		_roleManager = roleManager;
	}

	public async Task InitialiseAsync()
	{
		try
		{
			if (_context.Database.IsNpgsql())
			{
				await _context.Database.MigrateAsync();
			}
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "An error occurred while initialising the database.");
			throw;
		}
	}

	public async Task SeedAsync()
	{
		try
		{
			await TrySeedAsync();
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "An error occurred while seeding the database.");
			throw;
		}
	}

	public async Task TrySeedAsync()
	{
		// Default roles
		var adminRole = new IdentityRole("Admin");

		if (_roleManager.Roles.All(r => r.Name != adminRole.Name))
		{
			await _roleManager.CreateAsync(adminRole);
		}

		// Default users
		var admin = new IdentityUser
		{
			UserName = "admin@local",
			Email = "admin@local",
			EmailConfirmed = true
		};

		if (_userManager.Users.All(u => u.UserName != admin.UserName))
		{
			await _userManager.CreateAsync(admin, "Admin123!");
			await _userManager.AddToRolesAsync(admin, new[] { adminRole.Name! });
		}

		if (!_context.Clients.Any())
		{
			_context.Clients.Add(new Client
			{
				Id = new Guid("05004bd2-18d9-402f-9a1b-673fcf1d46e7"),
				Name = "Default",
				Secret = null
			});
			await _context.SaveChangesAsync();
		}

		// Default data
		// Seed, if necessary
		if (!_context.TodoLists.Any())
		{
			_context.TodoLists.Add(new TodoList
			{
				Title = "Todo List",
				Items =
				{
					new TodoItem { Title = "Make a todo list 📃" },
					new TodoItem { Title = "Check off the first item ✅" },
					new TodoItem { Title = "Realize you've already done two things on the list! 🤯"},
					new TodoItem { Title = "Reward yourself with a nice, long nap 🏆" },
				}
			});

			Recipe recipe = new Recipe("Appeltaart", "subAppelTaart", false, "descriptie", 20);
			_context.Recipes.Add(recipe);

			await _context.SaveChangesAsync();
		}
	}
}
