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
			_logger.LogError(ex, "An error occurred while initializing the database.");
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
			
			Category dessert = new()
			{
				Name = "Dessert"
			};
			Category ontbijt = new()
			{
				Name = "Ontbijt"
			};
			Category diner = new()
			{
				Name = "Diner"
			};
			Category lunch = new()
			{
				Name = "Lunch"
			};
			Category drankje = new()
			{
				Name = "Drankje"
			};
			Category borrelhapje = new()
			{
				Name = "Borrelhapje"
			};

			_context.Recipes.Add(new Recipe
			{
				Name = "Appeltaart",
				SubName = "subAppelTaart",
				PlusRecipe = false,
				Description = "descriptie",
				PrepTimeMinutes = 20,
				Categories = new List<Category>
				{
					dessert
				}
			});
			_context.Recipes.Add(new Recipe
			{
				Name = "Marrokaanse quinoa salade",
				SubName = "salade",
				PlusRecipe = true,
				Description = "descriptie",
				PrepTimeMinutes = 20,
				Categories = new List<Category>
				{
					ontbijt,
					diner
				}
			});
			_context.Recipes.Add(new Recipe
			{
				Name = "Beef and Broccoli Stir Fry",
				SubName = "",
				PlusRecipe = false,
				Description = "Thinly sliced beef and tender-crisp broccoli florets in a savory sauce served over steamed rice.",
				PrepTimeMinutes = 25,
				Categories = new List<Category>
				{
					diner
				}
			});
			_context.Recipes.Add(new Recipe
			{
				Name = "Pesto Pasta Salad",
				SubName = "",
				PlusRecipe = true,
				Description = "Rotini pasta tossed with fresh basil pesto, cherry tomatoes, and diced mozzarella cheese.",
				PrepTimeMinutes = 20,
				Categories = new List<Category>
				{
					ontbijt,
					diner
				}
			});
			
			_context.Recipes.Add(new Recipe
			{
				Name = "Creamy Tomato Soup",
				SubName = "",
				PlusRecipe = false,
				Description = "Comforting tomato soup made with fresh tomatoes, cream, and a hint of basil.",
				PrepTimeMinutes = 35,
				Categories = new List<Category>
				{
					diner
				}
			});
			
			_context.Recipes.Add(new Recipe
			{
				Name = "Chicken Parmesan",
				SubName = "",
				PlusRecipe = true,
				Description = "Breaded chicken breasts smothered in marinara sauce and melted mozzarella cheese.",
				PrepTimeMinutes = 40,
				Categories = new List<Category>
				{
					diner
				}
			});
			
			_context.Recipes.Add(new Recipe
			{
				Name = "Sheet Pan Fajitas",
				SubName = "",
				PlusRecipe = false,
				Description = "Flavorful fajitas made with seasoned chicken, bell peppers, and onions, baked on a sheet pan for easy cleanup.",
				PrepTimeMinutes = 30,
				Categories = new List<Category>
				{
					diner,
					lunch
				}
			});
			
			_context.Recipes.Add(new Recipe
			{
				Name = "Vegetable Lasagna",
				SubName = "",
				PlusRecipe = true,
				Description = "A classic lasagna with layers of pasta, ricotta cheese, and a medley of sautéed vegetables.",
				PrepTimeMinutes = 60,
				Categories = new List<Category>
				{
					diner
				}
			});
			
			_context.Recipes.Add(new Recipe
			{
				Name = "Beef Stroganoff",
				SubName = "",
				PlusRecipe = true,
				Description = "Tender strips of beef in a creamy mushroom sauce served over egg noodles.",
				PrepTimeMinutes = 50,
				Categories = new List<Category>
				{
					diner
				}
			});
			
			_context.Recipes.Add(new Recipe
			{
				Name = "Baked Ziti",
				SubName = "",
				PlusRecipe = true,
				Description = "Ziti pasta baked with tomato sauce, ground beef, and mozzarella cheese.",
				PrepTimeMinutes = 45,
				Categories = new List<Category>
				{
					borrelhapje
				}
			});
			
			_context.Recipes.Add(new Recipe
			{
				Name = "Slow Cooker Pulled Pork",
				SubName = "",
				PlusRecipe = false,
				Description = "Tender and juicy pulled pork slow-cooked to perfection, perfect for sandwiches or tacos.",
				PrepTimeMinutes = 360,
				Categories = new List<Category>
				{
					diner
				}
			});
			
			_context.Recipes.Add(new Recipe
			{
				Name = "Sausage and Peppers",
				SubName = "",
				PlusRecipe = false,
				Description = "Sausage links cooked with bell peppers and onions, served over steamed rice or on a hoagie roll.",
				PrepTimeMinutes = 35,
				Categories = new List<Category>
				{
					borrelhapje
				}
			});
			
			_context.Recipes.Add(new Recipe
			{
				Name = "Chicken Enchiladas",
				SubName = "",
				PlusRecipe = true,
				Description = "Shredded chicken and cheese wrapped in corn tortillas and baked with enchilada sauce.",
				PrepTimeMinutes = 40,
				Categories = new List<Category>
				{
					diner,
					dessert
				}
			});
			
			_context.Recipes.Add(new Recipe
			{
				Name = "Greek Salad",
				SubName = "",
				PlusRecipe = true,
				Description = "A refreshing salad with cucumber, tomatoes, feta cheese, and kalamata olives tossed in a lemon and olive oil dressing.",
				PrepTimeMinutes = 15,
				Categories = new List<Category>
				{
					lunch
				}
			});
			
			_context.Recipes.Add(new Recipe
			{
				Name = "Meatball Subs",
				SubName = "",
				PlusRecipe = false,
				Description = "Meatballs in marinara sauce served on a toasted hoagie roll with melted mozzarella cheese.",
				PrepTimeMinutes = 30,
				Categories = new List<Category>
				{
					diner
				}
			});
			
			_context.Recipes.Add(new Recipe
			{
				Name = "Salmon Cakes",
				SubName = "",
				PlusRecipe = true,
				Description = "Crispy salmon cakes made with fresh salmon, breadcrumbs, and seasonings, served with a side of tartar sauce.",
				PrepTimeMinutes = 25,
				Categories = new List<Category>
				{
					dessert
				}
			});
			
			_context.Recipes.Add(new Recipe
			{
				Name = "Chicken Alfredo",
				SubName = "",
				PlusRecipe = true,
				Description = "Fettuccine pasta smothered in a creamy Alfredo sauce with grilled chicken and broccoli.",
				PrepTimeMinutes = 35,
				Categories = new List<Category>
				{
					diner
				}
			});
			

			await _context.SaveChangesAsync();
		}
	}
}
