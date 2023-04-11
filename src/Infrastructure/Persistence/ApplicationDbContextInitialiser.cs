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
				Description = descriptie,
				PrepTimeMinutes = 20,
				Categories = new List<Category>
				{
					dessert
				}
			});

			Recipe recipe2 = new Recipe("Marrokaanse quinoa salade", "salade", true, "descriptie", 20);
			recipe2.Categories.Add(ontbijt);
			recipe2.Categories.Add(diner);

			Recipe r6 = new Recipe("Beef and Broccoli Stir Fry", "", false, "Thinly sliced beef and tender-crisp broccoli florets in a savory sauce served over steamed rice.", 25);
			r6.Categories.Add(diner);
			
			Recipe r7 = new Recipe("Pesto Pasta Salad", "", true, "Rotini pasta tossed with fresh basil pesto, cherry tomatoes, and diced mozzarella cheese.", 20);
			r7.Categories.Add(ontbijt);
			r7.Categories.Add(diner);
			Recipe r8 = new Recipe("Creamy Tomato Soup", "", false, "Comforting tomato soup made with fresh tomatoes, cream, and a hint of basil.", 35);
			r8.Categories.Add(diner);
			Recipe r9 = new Recipe("Chicken Parmesan", "", true, "Breaded chicken breasts smothered in marinara sauce and melted mozzarella cheese.", 40);
			r9.Categories.Add(diner);
			Recipe r10 = new Recipe("Sheet Pan Fajitas", "", false, "Flavorful fajitas made with seasoned chicken, bell peppers, and onions, baked on a sheet pan for easy cleanup.", 30);
			r10.Categories.Add(diner);
			r10.Categories.Add(lunch);
			Recipe r11 = new Recipe("Vegetable Lasagna", "", true, "A classic lasagna with layers of pasta, ricotta cheese, and a medley of sautéed vegetables.", 60);
			r11.Categories.Add(diner);
			Recipe r12 = new Recipe("Beef Stroganoff", "", true, "Tender strips of beef in a creamy mushroom sauce served over egg noodles.", 50);
			r12.Categories.Add(diner);
			Recipe r13 = new Recipe("Baked Ziti", "", true, "Ziti pasta baked with tomato sauce, ground beef, and mozzarella cheese.", 45);
			r13.Categories.Add(borrelhapje);
			Recipe r14 = new Recipe("Slow Cooker Pulled Pork", "", false, "Tender and juicy pulled pork slow-cooked to perfection, perfect for sandwiches or tacos.", 360);
			r14.Categories.Add(diner);
			Recipe r15 = new Recipe("Sausage and Peppers", "", false, "Sausage links cooked with bell peppers and onions, served over steamed rice or on a hoagie roll.", 35);
			r15.Categories.Add(borrelhapje);
			Recipe r16 = new Recipe("Chicken Enchiladas", "", true, "Shredded chicken and cheese wrapped in corn tortillas and baked with enchilada sauce.", 40);
			r16.Categories.Add(diner);
			r16.Categories.Add(dessert);
			Recipe r17 = new Recipe("Greek Salad", "", true, "A refreshing salad with cucumber, tomatoes, feta cheese, and kalamata olives tossed in a lemon and olive oil dressing.", 15);
			r17.Categories.Add(lunch);
			Recipe r18 = new Recipe("Meatball Subs", "", false, "Meatballs in marinara sauce served on a toasted hoagie roll with melted mozzarella cheese.", 30);
			r18.Categories.Add(diner);
			Recipe r19 = new Recipe("Salmon Cakes", "", true, "Crispy salmon cakes made with fresh salmon, breadcrumbs, and seasonings, served with a side of tartar sauce.", 25); 
			r19.Categories.Add(dessert);
			Recipe r20 = new Recipe("Chicken Alfredo", "", true, "Fettuccine pasta smothered in a creamy Alfredo sauce with grilled chicken and broccoli.", 35);
			r20.Categories.Add(diner);


			_context.Recipes.Add(recipe);
			_context.Recipes.Add(recipe2);
			_context.Recipes.Add(r6);
			_context.Recipes.Add(r7);
			_context.Recipes.Add(r8);
			_context.Recipes.Add(r9);
			_context.Recipes.Add(r10);
			_context.Recipes.Add(r11);
			_context.Recipes.Add(r12);
			_context.Recipes.Add(r13);
			_context.Recipes.Add(r14);
			_context.Recipes.Add(r15);
			_context.Recipes.Add(r16);
			_context.Recipes.Add(r17);
			_context.Recipes.Add(r18);
			_context.Recipes.Add(r19);
			_context.Recipes.Add(r20);
			
			

			await _context.SaveChangesAsync();
		}
	}
}
