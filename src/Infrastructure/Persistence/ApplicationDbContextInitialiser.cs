using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities;
using Template.Infrastructure.OAuth2;
using IdentityUser = Template.Infrastructure.Identity.IdentityUser;

namespace Template.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
	private readonly ApplicationDbContext _context;
	private readonly ILogger<ApplicationDbContextInitialiser> _logger;
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly UserManager<IdentityUser> _userManager;

	public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context,
		UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
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
		IdentityRole adminRole = new("Admin");

		if (_roleManager.Roles.All(r => r.Name != adminRole.Name))
		{
			await _roleManager.CreateAsync(adminRole);
		}

		// Default users
		IdentityUser admin = new()
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
		// COMPLETE DUMMY DATA
		if (!_context.Recipes.Any())
		{
			_context.Recipes.Add(new Recipe
			{
				Name = "Ezelsoep",
				SubName = "Herlijke ezelsoep met boeljon",
				PlusRecipe = true,
				PrepDifficulties = new List<string>
				{
					"Proffesioneel",
					"Moeilijk"
				},
				PrepTimeMinutes = 1440,
				Requirements = new List<Requirement>
				{
					new()
					{
						Name = "Pan"
					},
					new()
					{
						Name = "Fornuis"
					},
					new()
					{
						Name = "Enorme soeplepel"
					}
				},
				Seasons = new List<Season>
				{
					new()
					{
						SeasonName = "Winter"
					},
					new()
					{
						SeasonName = "Lente"
					},
					new()
					{
						SeasonName = "Zomer"
					},
					new()
					{
						SeasonName = "Herfst"
					}
				},
				Categories = new List<string>
				{
					"dinner"
				},
				CookingSteps = new List<CookingStep>
				{
					new()
					{
						Description = "pak ezel"
					},
					new()
					{
						Description = "gooi hem in de pan"
					},
					new()
					{
						Description = "eten maar"
					}
				},
				Description = "Heerlijke ezelsoep",
				Ingredients = new List<Ingredient>
				{
					new()
					{
						ingredientName = "ezel",
						Amount = "zoveel je wil",
						allergies = new List<string>
						{
							"ezel",
							"vlees"
						}
					},
					new()
					{
						ingredientName = "boeljon",
						Amount = "1 liter",
						allergies = new List<string>
						{
							"water",
							"zout"
						}
					}
				}
			});

			_context.Recipes.Add(new Recipe
			{
				Name = "Beef Stew",
				SubName = "Classic beef stew with vegetables",
				PlusRecipe = true,
				PrepDifficulties = new List<string>
				{
					"Intermediate",
					"Moderate"
				},
				PrepTimeMinutes = 120,
				Requirements = new List<Requirement>
				{
					new()
					{
						Name = "Large pot"
					},
					new()
					{
						Name = "Stove"
					},
					new()
					{
						Name = "Wooden spoon"
					}
				},
				Seasons = new List<Season>
				{
					new()
					{
						SeasonName = "Winter"
					},
					new()
					{
						SeasonName = "Fall"
					}
				},
				Categories = new List<string>
				{
					"dinner",
					"comfort food"
				},
				CookingSteps = new List<CookingStep>
				{
					new()
					{
						Description = "Cut beef into cubes"
					},
					new()
					{
						Description = "Brown beef in pot"
					},
					new()
					{
						Description = "Add vegetables and beef broth"
					},
					new()
					{
						Description = "Simmer for 1.5 hours"
					},
					new()
					{
						Description = "Season with salt and pepper"
					}
				},
				Description = "Classic beef stew",
				Ingredients = new List<Ingredient>
				{
					new()
					{
						ingredientName = "beef",
						Amount = "1.5 pounds",
						allergies = new List<string>
						{
							"meat"
						}
					},
					new()
					{
						ingredientName = "carrots",
						Amount = "2 cups",
						allergies = new List<string>
						{
							"vegetable"
						}
					},
					new()
					{
						ingredientName = "potatoes",
						Amount = "2 cups",
						allergies = new List<string>
						{
							"vegetable"
						}
					},
					new()
					{
						ingredientName = "onion",
						Amount = "1",
						allergies = new List<string>
						{
							"vegetable"
						}
					},
					new()
					{
						ingredientName = "beef broth",
						Amount = "4 cups",
						allergies = new List<string>
						{
							"liquid"
						}
					},
					new()
					{
						ingredientName = "salt",
						Amount = "to taste",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "pepper",
						Amount = "to taste",
						allergies = new List<string>
						{
							"none"
						}
					}
				}
			});

			_context.Recipes.Add(new Recipe
			{
				Name = "Thai Green Curry",
				SubName = "Creamy and aromatic Thai green curry",
				PlusRecipe = false,
				PrepDifficulties = new List<string>
				{
					"Intermediate",
					"Easy"
				},
				PrepTimeMinutes = 30,
				Requirements = new List<Requirement>
				{
					new()
					{
						Name = "Wok or Large Frying Pan"
					},
					new()
					{
						Name = "Rice Cooker"
					},
					new()
					{
						Name = "Chopping Board"
					},
					new()
					{
						Name = "Knife"
					}
				},
				Seasons = new List<Season>
				{
					new()
					{
						SeasonName = "All Seasons"
					}
				},
				Categories = new List<string>
				{
					"dinner",
					"lunch"
				},
				CookingSteps = new List<CookingStep>
				{
					new()
					{
						Description =
							"Heat oil in a wok or large frying pan over medium heat. Add green curry paste and stir-fry for 1 minute or until fragrant."
					},
					new()
					{
						Description = "Add chicken and stir-fry for 2-3 minutes or until browned. Add coconut milk and bring to the boil."
					},
					new()
					{
						Description = "Add eggplant, bamboo shoots and sugar, and stir-fry for 1-2 minutes or until just tender."
					},
					new()
					{
						Description = "Add fish sauce, lime juice and basil leaves. Stir until well combined."
					}
				},
				Description = "Aromatic and flavorful Thai green curry that's easy to make and perfect for any meal.",
				Ingredients = new List<Ingredient>
				{
					new()
					{
						ingredientName = "Green curry paste",
						Amount = "2-3 tbsp",
						allergies = new List<string>
						{
							"shrimp paste"
						}
					},
					new()
					{
						ingredientName = "Chicken breast",
						Amount = "500g",
						allergies = new List<string>
						{
							"chicken"
						}
					},
					new()
					{
						ingredientName = "Coconut milk",
						Amount = "1 can (400ml)",
						allergies = new List<string>
						{
							"coconut"
						}
					},
					new()
					{
						ingredientName = "Eggplant",
						Amount = "1 large, diced",
						allergies = new List<string>
						{
							"nightshade"
						}
					},
					new()
					{
						ingredientName = "Bamboo shoots",
						Amount = "1 can (400g), drained and rinsed",
						allergies = new List<string>
						{
							"bamboo"
						}
					},
					new()
					{
						ingredientName = "Sugar",
						Amount = "1 tsp",
						allergies = new List<string>
						{
							"sugar"
						}
					}
				}
			});

			_context.Recipes.Add(new Recipe
			{
				Name = "Sweet Potato Curry",
				SubName = "Delicious and healthy curry made with sweet potatoes",
				PlusRecipe = true,
				PrepDifficulties = new List<string>
				{
					"Easy",
					"Quick"
				},
				PrepTimeMinutes = 30,
				Requirements = new List<Requirement>
				{
					new()
					{
						Name = "Pot"
					},
					new()
					{
						Name = "Stove"
					},
					new()
					{
						Name = "Cutting board"
					},
					new()
					{
						Name = "Knife"
					}
				},
				Seasons = new List<Season>
				{
					new()
					{
						SeasonName = "Fall"
					},
					new()
					{
						SeasonName = "Winter"
					},
					new()
					{
						SeasonName = "Spring"
					},
					new()
					{
						SeasonName = "Summer"
					}
				},
				Categories = new List<string>
				{
					"Vegetarian",
					"Vegan",
					"Dinner",
					"Healthy"
				},
				CookingSteps = new List<CookingStep>
				{
					new()
					{
						Description = "Peel and cut the sweet potatoes into bite-sized pieces"
					},
					new()
					{
						Description = "Chop the onion and garlic"
					},
					new()
					{
						Description = "Heat the oil in a pot and add the onion and garlic. Cook until fragrant."
					},
					new()
					{
						Description = "Add the sweet potatoes and stir until coated in the onion and garlic mixture."
					},
					new()
					{
						Description = "Add the curry powder, cumin, coriander, and cinnamon. Stir well."
					},
					new()
					{
						Description = "Add the coconut milk and vegetable broth. Bring to a boil."
					},
					new()
					{
						Description = "Reduce heat and let simmer for 15-20 minutes, or until the sweet potatoes are tender."
					},
					new()
					{
						Description = "Serve with rice or naan bread."
					}
				},
				Description = "This sweet potato curry is a delicious and healthy dinner option that's easy to make!",
				Ingredients = new List<Ingredient>
				{
					new()
					{
						ingredientName = "Sweet potatoes",
						Amount = "2-3 medium",
						allergies = new List<string>
						{
							"None"
						}
					},
					new()
					{
						ingredientName = "Onion",
						Amount = "1 medium",
						allergies = new List<string>
						{
							"None"
						}
					},
					new()
					{
						ingredientName = "Garlic",
						Amount = "3 cloves",
						allergies = new List<string>
						{
							"None"
						}
					}
				}
			});

			_context.Recipes.Add(new Recipe
			{
				Name = "Lemon Herb Roasted Chicken",
				SubName = "A deliciously juicy and flavorful roasted chicken",
				PlusRecipe = true,
				PrepDifficulties = new List<string>
				{
					"Intermediate",
					"Moderate"
				},
				PrepTimeMinutes = 120,
				Requirements = new List<Requirement>
				{
					new()
					{
						Name = "Roasting pan"
					},
					new()
					{
						Name = "Oven"
					},
					new()
					{
						Name = "Kitchen twine"
					}
				},
				Seasons = new List<Season>
				{
					new()
					{
						SeasonName = "Any"
					}
				},
				Categories = new List<string>
				{
					"dinner",
					"entertaining"
				},
				CookingSteps = new List<CookingStep>
				{
					new()
					{
						Description = "Preheat the oven to 375 degrees F (190 degrees C)."
					},
					new()
					{
						Description = "Remove any giblets from the chicken and rinse it inside and out. Pat dry with paper towels."
					},
					new()
					{
						Description = "In a small bowl, mix together butter, minced garlic, lemon juice, and herbs until well combined."
					},
					new()
					{
						Description = "Season the chicken inside and out with salt and pepper."
					},
					new()
					{
						Description = "Rub the butter mixture all over the chicken and under the skin."
					},
					new()
					{
						Description = "Tie the legs together with kitchen twine and tuck the wings under the body."
					},
					new()
					{
						Description = "Place the chicken breast-side up in a roasting pan."
					},
					new()
					{
						Description =
							"Roast the chicken for approximately 1 hour and 30 minutes, or until a thermometer inserted into the thickest part of the thigh registers 165 degrees F (75 degrees C)."
					},
					new()
					{
						Description = "Let the chicken rest for 10 minutes before carving and serving."
					}
				},
				Description = "A juicy and flavorful roasted chicken that's perfect for any occasion.",
				Ingredients = new List<Ingredient>
				{
					new()
					{
						ingredientName = "Whole chicken",
						Amount = "1 (3-4 pound)",
						allergies = new List<string>
						{
							"Poultry"
						}
					},
					new()
					{
						ingredientName = "Butter",
						Amount = "4 tablespoons, softened",
						allergies = new List<string>
						{
							"Dairy"
						}
					},
					new()
					{
						ingredientName = "Garlic",
						Amount = "2 cloves, minced",
						allergies = new List<string>
						{
							"None"
						}
					},
					new()
					{
						ingredientName = "Lemon juice",
						Amount = "2 tablespoons",
						allergies = new List<string>
						{
							"Citrus"
						}
					},
					new()
					{
						ingredientName = "Herbs",
						Amount = "2 tablespoons, finely chopped (such as rosemary, thyme, and parsley)",
						allergies = new List<string>
						{
							"None"
						}
					},
					new()
					{
						ingredientName = "Salt",
						Amount = "To taste",
						allergies = new List<string>
						{
							"None"
						}
					}
				}
			});

			_context.Recipes.Add(new Recipe
			{
				Name = "Spicy Garlic Shrimp",
				SubName = "Delicious spicy garlic shrimp served with rice",
				PlusRecipe = true,
				PrepDifficulties = new List<string>
				{
					"Easy",
					"Intermediate"
				},
				PrepTimeMinutes = 30,
				Requirements = new List<Requirement>
				{
					new()
					{
						Name = "Large skillet"
					},
					new()
					{
						Name = "Stove"
					},
					new()
					{
						Name = "Cutting board"
					},
					new()
					{
						Name = "Sharp knife"
					}
				},
				Seasons = new List<Season>
				{
					new()
					{
						SeasonName = "Summer"
					},
					new()
					{
						SeasonName = "Fall"
					},
					new()
					{
						SeasonName = "Winter"
					},
					new()
					{
						SeasonName = "Spring"
					}
				},
				Categories = new List<string>
				{
					"dinner",
					"seafood",
					"spicy"
				},
				CookingSteps = new List<CookingStep>
				{
					new()
					{
						Description = "Peel and devein shrimp, set aside"
					},
					new()
					{
						Description = "Heat oil in a large skillet over medium heat"
					},
					new()
					{
						Description = "Add garlic and sauté for 1-2 minutes"
					},
					new()
					{
						Description = "Add shrimp to the skillet and cook for 2-3 minutes"
					},
					new()
					{
						Description = "Add red pepper flakes and salt, cook for another 1-2 minutes"
					},
					new()
					{
						Description = "Serve over cooked rice"
					}
				},
				Description = "Spicy Garlic Shrimp",
				Ingredients = new List<Ingredient>
				{
					new()
					{
						ingredientName = "Shrimp",
						Amount = "1 pound",
						allergies = new List<string>
						{
							"shellfish"
						}
					},
					new()
					{
						ingredientName = "Garlic",
						Amount = "6 cloves, minced",
						allergies = new List<string>
						{
							"garlic"
						}
					},
					new()
					{
						ingredientName = "Olive oil",
						Amount = "2 tablespoons",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "Red pepper flakes",
						Amount = "1/2 teaspoon",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "Salt",
						Amount = "1/2 teaspoon",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "Rice",
						Amount = "2 cups cooked",
						allergies = new List<string>
						{
							"none"
						}
					}
				}
			});

			_context.Recipes.Add(new Recipe
			{
				Name = "Vegan Lentil Soup",
				SubName = "Hearty and flavorful vegan lentil soup",
				PlusRecipe = true,
				PrepDifficulties = new List<string>
				{
					"Easy"
				},
				PrepTimeMinutes = 60,
				Requirements = new List<Requirement>
				{
					new()
					{
						Name = "Large pot"
					},
					new()
					{
						Name = "Stove"
					},
					new()
					{
						Name = "Cutting board"
					},
					new()
					{
						Name = "Sharp knife"
					}
				},
				Seasons = new List<Season>
				{
					new()
					{
						SeasonName = "Winter"
					},
					new()
					{
						SeasonName = "Fall"
					}
				},
				Categories = new List<string>
				{
					"soup",
					"vegan",
					"lentils",
					"healthy"
				},
				CookingSteps = new List<CookingStep>
				{
					new()
					{
						Description = "Rinse and drain lentils, set aside"
					},
					new()
					{
						Description = "Heat oil in a large pot over medium heat"
					},
					new()
					{
						Description = "Add onion, carrots, and celery to the pot, sauté for 5-7 minutes"
					},
					new()
					{
						Description = "Add garlic, cumin, coriander, and turmeric, cook for another minute"
					},
					new()
					{
						Description = "Add lentils, diced tomatoes, vegetable broth, and bay leaves, bring to a boil"
					},
					new()
					{
						Description = "Reduce heat and simmer for 30-40 minutes or until lentils are tender"
					},
					new()
					{
						Description = "Remove bay leaves and season with salt and pepper to taste"
					},
					new()
					{
						Description = "Serve hot with a slice of bread or crackers"
					}
				},
				Description = "Vegan Lentil Soup",
				Ingredients = new List<Ingredient>
				{
					new()
					{
						ingredientName = "Green lentils",
						Amount = "1 cup",
						allergies = new List<string>
						{
							"lentils"
						}
					},
					new()
					{
						ingredientName = "Onion",
						Amount = "1 medium, diced",
						allergies = new List<string>
						{
							"onion"
						}
					},
					new()
					{
						ingredientName = "Carrots",
						Amount = "2 medium, diced",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "Celery",
						Amount = "2 stalks, diced",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "Garlic",
						Amount = "3 cloves, minced",
						allergies = new List<string>
						{
							"garlic"
						}
					},
					new()
					{
						ingredientName = "Cumin",
						Amount = "1 teaspoon",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "Coriander",
						Amount = "1 teaspoon",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "Turmeric",
						Amount = "1/2 teaspoon",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "Diced tomatoes",
						Amount = "14.5 oz can",
						allergies = new List<string>
						{
							"none"
						}
					}
				}
			});

			_context.Recipes.Add(new Recipe
			{
				Name = "Sweet Potato and Chickpea Curry",
				SubName = "A spicy and delicious vegetarian curry",
				PlusRecipe = true,
				PrepDifficulties = new List<string>
				{
					"Easy",
					"Quick"
				},
				PrepTimeMinutes = 30,
				Requirements = new List<Requirement>
				{
					new()
					{
						Name = "Large pot"
					},
					new()
					{
						Name = "Stove"
					},
					new()
					{
						Name = "Chopping board"
					},
					new()
					{
						Name = "Sharp knife"
					}
				},
				Seasons = new List<Season>
				{
					new()
					{
						SeasonName = "Fall"
					},
					new()
					{
						SeasonName = "Winter"
					},
					new()
					{
						SeasonName = "Spring"
					}
				},
				Categories = new List<string>
				{
					"dinner",
					"vegetarian",
					"vegan",
					"gluten-free"
				},
				CookingSteps = new List<CookingStep>
				{
					new()
					{
						Description = "Peel and chop the sweet potatoes into small pieces."
					},
					new()
					{
						Description = "Heat oil in a large pot over medium heat. Add chopped onions and sauté until they're soft and translucent."
					},
					new()
					{
						Description = "Add garlic, ginger, and curry powder to the pot and stir for a minute until fragrant."
					},
					new()
					{
						Description =
							"Add the chopped sweet potatoes to the pot and stir well to coat them in the spices. Cook for 5 minutes, stirring occasionally."
					},
					new()
					{
						Description =
							"Add the can of drained and rinsed chickpeas to the pot, along with a can of diced tomatoes, coconut milk, and vegetable stock. Stir everything together."
					},
					new()
					{
						Description =
							"Bring the curry to a boil, then reduce the heat and let it simmer for 15-20 minutes, or until the sweet potatoes are cooked through."
					},
					new()
					{
						Description = "Serve the curry with rice or naan bread and garnish with chopped fresh cilantro, if desired."
					}
				},
				Description = "This vegetarian curry is packed with flavor and is easy to make!",
				Ingredients = new List<Ingredient>
				{
					new()
					{
						ingredientName = "Sweet potatoes",
						Amount = "2 large",
						allergies = new List<string>
						{
							"None"
						}
					},
					new()
					{
						ingredientName = "Onion",
						Amount = "1 large",
						allergies = new List<string>
						{
							"None"
						}
					},
					new()
					{
						ingredientName = "Garlic",
						Amount = "3 cloves",
						allergies = new List<string>
						{
							"None"
						}
					},
					new()
					{
						ingredientName = "Ginger",
						Amount = "1 thumb-sized piece",
						allergies = new List<string>
						{
							"None"
						}
					},
					new()
					{
						ingredientName = "Curry powder",
						Amount = "2 tbsp",
						allergies = new List<string>
						{
							"None"
						}
					}
				}
			});

			_context.Recipes.Add(new Recipe
			{
				Name = "Spicy Cauliflower Rice Bowl",
				SubName = "A flavorful and healthy vegan dish",
				PlusRecipe = true,
				PrepDifficulties = new List<string>
				{
					"Easy",
					"Quick"
				},
				PrepTimeMinutes = 20,
				Requirements = new List<Requirement>
				{
					new()
					{
						Name = "Large skillet"
					},
					new()
					{
						Name = "Cutting board"
					},
					new()
					{
						Name = "Sharp knife"
					},
					new()
					{
						Name = "Wooden spoon"
					}
				},
				Seasons = new List<Season>
				{
					new()
					{
						SeasonName = "Any"
					}
				},
				Categories = new List<string>
				{
					"lunch",
					"dinner",
					"vegan"
				},
				CookingSteps = new List<CookingStep>
				{
					new()
					{
						Description = "Rinse and chop cauliflower into small pieces"
					},
					new()
					{
						Description = "Heat oil in a large skillet over medium heat"
					},
					new()
					{
						Description = "Add garlic, onion, and cauliflower to the skillet and cook for 5 minutes"
					},
					new()
					{
						Description = "Add bell pepper, black beans, and spices to the skillet and cook for an additional 3 minutes"
					},
					new()
					{
						Description = "Serve with brown rice or quinoa and top with avocado slices and cilantro"
					}
				},
				Description = "A delicious and healthy vegan meal",
				Ingredients = new List<Ingredient>
				{
					new()
					{
						ingredientName = "Cauliflower",
						Amount = "1 head",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "Garlic",
						Amount = "2 cloves",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "Onion",
						Amount = "1/2",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "Bell pepper",
						Amount = "1",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "Black beans",
						Amount = "1 can",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "Chili powder",
						Amount = "1 tsp",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "Cumin",
						Amount = "1 tsp",
						allergies = new List<string>
						{
							"none"
						}
					}
				}
			});

			_context.Recipes.Add(new Recipe
			{
				Name = "Creamy Tomato Soup",
				SubName = "A comforting soup with fresh tomatoes and cream",
				PlusRecipe = false,
				PrepDifficulties = new List<string>
				{
					"Easy",
					"Beginner-friendly"
				},
				PrepTimeMinutes = 30,
				Requirements = new List<Requirement>
				{
					new()
					{
						Name = "Large pot"
					},
					new()
					{
						Name = "Blender"
					},
					new()
					{
						Name = "Stirring spoon"
					}
				},
				Seasons = new List<Season>
				{
					new()
					{
						SeasonName = "Fall"
					},
					new()
					{
						SeasonName = "Winter"
					}
				},
				Categories = new List<string>
				{
					"lunch",
					"dinner"
				},
				CookingSteps = new List<CookingStep>
				{
					new()
					{
						Description = "Heat olive oil in a large pot over medium heat."
					},
					new()
					{
						Description = "Add onion, garlic, and carrots. Cook until vegetables are tender."
					},
					new()
					{
						Description = "Add fresh tomatoes, tomato paste, and chicken broth. Bring to a boil."
					},
					new()
					{
						Description = "Reduce heat and let simmer for 20 minutes."
					},
					new()
					{
						Description = "Use a blender to puree the soup until smooth."
					},
					new()
					{
						Description = "Add cream and stir to combine. Heat until warm."
					}
				},
				Description = "A comforting and creamy tomato soup that is easy to make.",
				Ingredients = new List<Ingredient>
				{
					new()
					{
						ingredientName = "olive oil",
						Amount = "2 tablespoons",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "onion",
						Amount = "1 medium, chopped",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "garlic",
						Amount = "3 cloves, minced",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "carrots",
						Amount = "2 medium, chopped",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "fresh tomatoes",
						Amount = "6 large, chopped",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "tomato paste",
						Amount = "2 tablespoons",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "chicken broth",
						Amount = "4 cups",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "heavy cream",
						Amount = "1/2 cup",
						allergies = new List<string>
						{
							"dairy"
						}
					}
				}
			});

			_context.Recipes.Add(new Recipe
			{
				Name = "Veggie Stir-Fry",
				SubName = "Quick and easy vegetarian stir-fry",
				PlusRecipe = true,
				PrepDifficulties = new List<string>
				{
					"Easy"
				},
				PrepTimeMinutes = 20,
				Requirements = new List<Requirement>
				{
					new()
					{
						Name = "Wok"
					},
					new()
					{
						Name = "Stirring spoon"
					}
				},
				Seasons = new List<Season>
				{
					new()
					{
						SeasonName = "All"
					}
				},
				Categories = new List<string>
				{
					"Dinner",
					"Vegetarian",
					"Quick and Easy"
				},
				CookingSteps = new List<CookingStep>
				{
					new()
					{
						Description = "Chop vegetables into bite-sized pieces (suggestions: bell peppers, broccoli, onions, mushrooms, carrots)"
					},
					new()
					{
						Description = "Heat oil in a wok over medium-high heat"
					},
					new()
					{
						Description = "Add vegetables and stir-fry for 5-7 minutes until slightly softened"
					},
					new()
					{
						Description = "Add soy sauce and hoisin sauce to taste"
					},
					new()
					{
						Description = "Continue stir-frying for another 2-3 minutes"
					},
					new()
					{
						Description = "Serve over rice or noodles"
					}
				},
				Description = "Quick and easy vegetarian stir-fry",
				Ingredients = new List<Ingredient>
				{
					new()
					{
						ingredientName = "Assorted vegetables",
						Amount = "2-3 cups",
						allergies = new List<string>
						{
							"None"
						}
					},
					new()
					{
						ingredientName = "Oil",
						Amount = "2 tbsp",
						allergies = new List<string>
						{
							"None"
						}
					},
					new()
					{
						ingredientName = "Soy sauce",
						Amount = "2-3 tbsp",
						allergies = new List<string>
						{
							"Soy"
						}
					},
					new()
					{
						ingredientName = "Hoisin sauce",
						Amount = "1-2 tbsp",
						allergies = new List<string>
						{
							"Soy",
							"Wheat"
						}
					},
					new()
					{
						ingredientName = "Rice or noodles",
						Amount = "As desired",
						allergies = new List<string>
						{
							"Wheat"
						}
					}
				}
			});

			_context.Recipes.Add(new Recipe
			{
				Name = "Lemon Garlic Shrimp Pasta",
				SubName = "Creamy and flavorful pasta with succulent shrimp",
				PlusRecipe = true,
				PrepDifficulties = new List<string>
				{
					"Easy",
					"Beginner"
				},
				PrepTimeMinutes = 30,
				Requirements = new List<Requirement>
				{
					new()
					{
						Name = "Large pot"
					},
					new()
					{
						Name = "Skillet"
					},
					new()
					{
						Name = "Cutting board"
					},
					new()
					{
						Name = "Sharp knife"
					}
				},
				Seasons = new List<Season>
				{
					new()
					{
						SeasonName = "Spring"
					},
					new()
					{
						SeasonName = "Summer"
					},
					new()
					{
						SeasonName = "Fall"
					},
					new()
					{
						SeasonName = "Winter"
					}
				},
				Categories = new List<string>
				{
					"pasta",
					"dinner"
				},
				CookingSteps = new List<CookingStep>
				{
					new()
					{
						Description = "Bring a large pot of salted water to boil."
					},
					new()
					{
						Description = "Add pasta and cook according to package instructions."
					},
					new()
					{
						Description = "While pasta is cooking, heat olive oil in a skillet over medium-high heat."
					},
					new()
					{
						Description = "Add shrimp and cook until pink, about 2-3 minutes per side."
					},
					new()
					{
						Description = "Remove shrimp from skillet and set aside."
					},
					new()
					{
						Description = "Add garlic and red pepper flakes to skillet and cook until fragrant, about 1 minute."
					},
					new()
					{
						Description = "Add heavy cream, lemon zest, and lemon juice to skillet and bring to a simmer."
					},
					new()
					{
						Description = "Add cooked pasta to skillet and toss to coat with sauce."
					},
					new()
					{
						Description = "Add shrimp back to skillet and toss to combine."
					},
					new()
					{
						Description = "Season with salt and black pepper to taste."
					},
					new()
					{
						Description = "Serve hot and garnish with chopped parsley."
					}
				},
				Description =
					"This Lemon Garlic Shrimp Pasta is creamy and flavorful, with succulent shrimp, tangy lemon, and a hint of garlic. Perfect for a quick and easy weeknight dinner!",
				Ingredients = new List<Ingredient>
				{
					new()
					{
						ingredientName = "pasta",
						Amount = "1 pound",
						allergies = new List<string>
						{
							"gluten"
						}
					},
					new()
					{
						ingredientName = "shrimp",
						Amount = "1 pound",
						allergies = new List<string>
						{
							"shellfish"
						}
					},
					new()
					{
						ingredientName = "olive oil",
						Amount = "2 tablespoons",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "garlic",
						Amount = "4 cloves, minced",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "red pepper flakes",
						Amount = "1/4 teaspoon",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "heavy cream",
						Amount = "1 cup",
						allergies = new List<string>
						{
							"none"
						}
					}
				}
			});

			_context.Recipes.Add(new Recipe
			{
				Name = "Spinach and Feta Stuffed Chicken",
				SubName = "Juicy and delicious chicken breast stuffed with spinach and feta cheese",
				PlusRecipe = false,
				PrepDifficulties = new List<string>
				{
					"Easy",
					"Beginner"
				},
				PrepTimeMinutes = 40,
				Requirements = new List<Requirement>
				{
					new()
					{
						Name = "Oven safe skillet"
					},
					new()
					{
						Name = "Oven"
					},
					new()
					{
						Name = "Meat thermometer"
					}
				},
				Seasons = new List<Season>
				{
					new()
					{
						SeasonName = "Any season"
					}
				},
				Categories = new List<string>
				{
					"dinner",
					"low carb",
					"gluten-free"
				},
				CookingSteps = new List<CookingStep>
				{
					new()
					{
						Description = "Preheat oven to 375°F"
					},
					new()
					{
						Description = "Heat an oven safe skillet over medium-high heat"
					},
					new()
					{
						Description = "Season the chicken breasts with salt and pepper on both sides"
					},
					new()
					{
						Description = "In a bowl, mix together the spinach, feta cheese, garlic, and olive oil"
					},
					new()
					{
						Description = "Stuff the spinach and feta mixture into the chicken breasts"
					},
					new()
					{
						Description = "Place the chicken breasts in the skillet and sear for 2-3 minutes per side"
					},
					new()
					{
						Description = "Transfer the skillet to the oven and bake for 25-30 minutes or until the internal temperature reaches 165°F"
					},
					new()
					{
						Description = "Remove from oven and let the chicken rest for 5 minutes before serving"
					}
				},
				Description = "Juicy and delicious chicken breast stuffed with spinach and feta cheese",
				Ingredients = new List<Ingredient>
				{
					new()
					{
						ingredientName = "boneless skinless chicken breasts",
						Amount = "4",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "spinach",
						Amount = "2 cups",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "feta cheese",
						Amount = "1/2 cup crumbled",
						allergies = new List<string>
						{
							"dairy"
						}
					},
					new()
					{
						ingredientName = "garlic",
						Amount = "2 cloves minced",
						allergies = new List<string>
						{
							"none"
						}
					}
				}
			});

			_context.Recipes.Add(new Recipe
			{
				Name = "Spicy Tofu Stir-Fry",
				SubName = "A delicious and healthy stir-fry",
				PlusRecipe = true,
				PrepDifficulties = new List<string>
				{
					"Easy"
				},
				PrepTimeMinutes = 30,
				Requirements = new List<Requirement>
				{
					new()
					{
						Name = "Wok"
					},
					new()
					{
						Name = "Knife"
					},
					new()
					{
						Name = "Cutting board"
					},
					new()
					{
						Name = "Bowl"
					},
					new()
					{
						Name = "Spoon"
					}
				},
				Seasons = new List<Season>
				{
					new()
					{
						SeasonName = "Any season"
					}
				},
				Categories = new List<string>
				{
					"dinner",
					"vegan",
					"healthy"
				},
				CookingSteps = new List<CookingStep>
				{
					new()
					{
						Description = "Press tofu to remove excess water"
					},
					new()
					{
						Description = "Cut tofu into cubes"
					},
					new()
					{
						Description = "Chop vegetables"
					},
					new()
					{
						Description = "Heat wok and add oil"
					},
					new()
					{
						Description = "Stir-fry vegetables until tender"
					},
					new()
					{
						Description = "Add tofu and stir-fry for a few minutes"
					},
					new()
					{
						Description = "Add sauce and stir-fry for another minute"
					},
					new()
					{
						Description = "Serve over rice"
					}
				},
				Description = "This spicy tofu stir-fry is a healthy and delicious dinner option that is easy to make.",
				Ingredients = new List<Ingredient>
				{
					new()
					{
						ingredientName = "tofu",
						Amount = "1 block",
						allergies = new List<string>
						{
							"soy"
						}
					},
					new()
					{
						ingredientName = "broccoli",
						Amount = "1 head",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "red bell pepper",
						Amount = "1",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "carrot",
						Amount = "1",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "green onion",
						Amount = "3",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "garlic",
						Amount = "3 cloves",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "ginger",
						Amount = "1 tsp",
						allergies = new List<string>
						{
							"none"
						}
					},
					new()
					{
						ingredientName = "soy sauce",
						Amount = "2 tbsp",
						allergies = new List<string>
						{
							"soy"
						}
					}
				}
			});

			_context.Recipes.Add(new Recipe
			{
				Name = "Spicy Tofu Stir-Fry",
				SubName = "A quick and easy stir-fry that's loaded with flavor",
				PlusRecipe = false,
				PrepDifficulties = new List<string>
				{
					"Easy",
					"Beginner"
				},
				PrepTimeMinutes = 20,
				Requirements = new List<Requirement>
				{
					new()
					{
						Name = "Wok"
					},
					new()
					{
						Name = "Knife"
					},
					new()
					{
						Name = "Cutting board"
					},
					new()
					{
						Name = "Spatula"
					}
				},
				Seasons = new List<Season>
				{
					new()
					{
						SeasonName = "All year"
					}
				},
				Categories = new List<string>
				{
					"dinner",
					"vegetarian",
					"gluten-free"
				},
				CookingSteps = new List<CookingStep>
				{
					new()
					{
						Description = "Cut tofu into small cubes and set aside"
					},
					new()
					{
						Description = "Heat a wok or large skillet over medium-high heat"
					},
					new()
					{
						Description = "Add oil to the wok and swirl to coat"
					},
					new()
					{
						Description = "Add garlic, ginger, and red pepper flakes and stir-fry for 30 seconds"
					},
					new()
					{
						Description = "Add tofu and stir-fry for 3-4 minutes or until lightly browned"
					},
					new()
					{
						Description = "Add bell pepper, onion, and snap peas and stir-fry for an additional 3-4 minutes"
					},
					new()
					{
						Description = "In a small bowl, whisk together soy sauce, hoisin sauce, and cornstarch"
					},
					new()
					{
						Description = "Add sauce to the wok and stir-fry for an additional minute or until the sauce thickens"
					},
					new()
					{
						Description = "Serve hot with rice or noodles"
					}
				},
				Description = "This spicy tofu stir-fry is a quick and easy vegetarian meal that's packed with flavor.",
				Ingredients = new List<Ingredient>
				{
					new()
					{
						ingredientName = "Tofu",
						Amount = "1 block",
						allergies = new List<string>
						{
							"Soy"
						}
					},
					new()
					{
						ingredientName = "Garlic",
						Amount = "2 cloves",
						allergies = new List<string>
						{
							"None"
						}
					},
					new()
					{
						ingredientName = "Ginger",
						Amount = "1 inch piece, minced",
						allergies = new List<string>
						{
							"None"
						}
					}
				}
			});
		}

		await _context.SaveChangesAsync();
	}
}
