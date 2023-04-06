﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Template.Infrastructure.Persistence;

#nullable disable

namespace Template.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CategoryRecipe", b =>
                {
                    b.Property<Guid>("allCategoriesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("recipesId")
                        .HasColumnType("uuid");

                    b.HasKey("allCategoriesId", "recipesId");

                    b.HasIndex("recipesId");

                    b.ToTable("CategoryRecipe");
                });

            modelBuilder.Entity("DietaryPreferenceRecipe", b =>
                {
                    b.Property<Guid>("allDietaryPreferencesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("recipesId")
                        .HasColumnType("uuid");

                    b.HasKey("allDietaryPreferencesId", "recipesId");

                    b.HasIndex("recipesId");

                    b.ToTable("DietaryPreferenceRecipe");
                });

            modelBuilder.Entity("IngredientRecipe", b =>
                {
                    b.Property<Guid>("allIngredientsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("recipesId")
                        .HasColumnType("uuid");

                    b.HasKey("allIngredientsId", "recipesId");

                    b.HasIndex("recipesId");

                    b.ToTable("IngredientRecipe");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("IdentityRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("IdentityRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("IdentityUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("IdentityUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("IdentityUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("IdentityUserTokens", (string)null);
                });

            modelBuilder.Entity("PrepDifficultyRecipe", b =>
                {
                    b.Property<Guid>("allPrepDifficultiesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("recipesId")
                        .HasColumnType("uuid");

                    b.HasKey("allPrepDifficultiesId", "recipesId");

                    b.HasIndex("recipesId");

                    b.ToTable("PrepDifficultyRecipe");
                });

            modelBuilder.Entity("RecipeSeason", b =>
                {
                    b.Property<Guid>("allSeasonsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("recipesId")
                        .HasColumnType("uuid");

                    b.HasKey("allSeasonsId", "recipesId");

                    b.HasIndex("recipesId");

                    b.ToTable("RecipeSeason");
                });

            modelBuilder.Entity("Template.Domain.Entities.Allergy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<string>("typeOfAllergy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Allergies");
                });

            modelBuilder.Entity("Template.Domain.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Template.Domain.Entities.CookingStep", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("RecipeId")
                        .HasColumnType("uuid");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("CookingSteps");
                });

            modelBuilder.Entity("Template.Domain.Entities.DietaryPreference", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("DietaryPreference");
                });

            modelBuilder.Entity("Template.Domain.Entities.Ingredient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("amount")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("productId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("productId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("Template.Domain.Entities.PrepDifficulty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PrepDifficulty");
                });

            modelBuilder.Entity("Template.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("amount")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Template.Domain.Entities.Recipe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("plusRecipe")
                        .HasColumnType("boolean");

                    b.Property<int>("prepTimeMinutes")
                        .HasColumnType("integer");

                    b.Property<string>("subname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("Template.Domain.Entities.Requirement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("RecipeId")
                        .HasColumnType("uuid");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("Requirements");
                });

            modelBuilder.Entity("Template.Domain.Entities.Season", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("season")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Seasons");
                });

            modelBuilder.Entity("Template.Domain.Entities.TodoItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<bool>("Done")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<Guid>("ListId")
                        .HasColumnType("uuid");

                    b.Property<string>("Note")
                        .HasColumnType("text");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("Reminder")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.HasIndex("ListId");

                    b.ToTable("TodoItems");
                });

            modelBuilder.Entity("Template.Domain.Entities.TodoList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.ToTable("TodoLists");
                });

            modelBuilder.Entity("Template.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("IdentityId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("isSubscriber")
                        .HasColumnType("boolean");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IdentityId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Template.Infrastructure.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("IdentityUsers", (string)null);
                });

            modelBuilder.Entity("Template.Infrastructure.OAuth2.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Secret")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Template.Infrastructure.OAuth2.RefreshToken", b =>
                {
                    b.Property<string>("Token")
                        .HasMaxLength(44)
                        .HasColumnType("character varying(44)");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<string>("IdentityId")
                        .HasColumnType("text");

                    b.Property<DateTime>("IssuedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Token");

                    b.HasIndex("ClientId");

                    b.HasIndex("IdentityId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("CategoryRecipe", b =>
                {
                    b.HasOne("Template.Domain.Entities.Category", null)
                        .WithMany()
                        .HasForeignKey("allCategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Template.Domain.Entities.Recipe", null)
                        .WithMany()
                        .HasForeignKey("recipesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DietaryPreferenceRecipe", b =>
                {
                    b.HasOne("Template.Domain.Entities.DietaryPreference", null)
                        .WithMany()
                        .HasForeignKey("allDietaryPreferencesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Template.Domain.Entities.Recipe", null)
                        .WithMany()
                        .HasForeignKey("recipesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IngredientRecipe", b =>
                {
                    b.HasOne("Template.Domain.Entities.Ingredient", null)
                        .WithMany()
                        .HasForeignKey("allIngredientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Template.Domain.Entities.Recipe", null)
                        .WithMany()
                        .HasForeignKey("recipesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Template.Infrastructure.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Template.Infrastructure.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Template.Infrastructure.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Template.Infrastructure.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PrepDifficultyRecipe", b =>
                {
                    b.HasOne("Template.Domain.Entities.PrepDifficulty", null)
                        .WithMany()
                        .HasForeignKey("allPrepDifficultiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Template.Domain.Entities.Recipe", null)
                        .WithMany()
                        .HasForeignKey("recipesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecipeSeason", b =>
                {
                    b.HasOne("Template.Domain.Entities.Season", null)
                        .WithMany()
                        .HasForeignKey("allSeasonsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Template.Domain.Entities.Recipe", null)
                        .WithMany()
                        .HasForeignKey("recipesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Template.Domain.Entities.Allergy", b =>
                {
                    b.HasOne("Template.Domain.Entities.Product", null)
                        .WithMany("allAllergies")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("Template.Domain.Entities.CookingStep", b =>
                {
                    b.HasOne("Template.Domain.Entities.Recipe", null)
                        .WithMany("cookingStep")
                        .HasForeignKey("RecipeId");
                });

            modelBuilder.Entity("Template.Domain.Entities.DietaryPreference", b =>
                {
                    b.HasOne("Template.Domain.Entities.User", null)
                        .WithMany("allDietaryPreferences")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Template.Domain.Entities.Ingredient", b =>
                {
                    b.HasOne("Template.Domain.Entities.Product", "product")
                        .WithMany()
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("product");
                });

            modelBuilder.Entity("Template.Domain.Entities.Recipe", b =>
                {
                    b.HasOne("Template.Domain.Entities.User", null)
                        .WithMany("favouriteRecipes")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Template.Domain.Entities.Requirement", b =>
                {
                    b.HasOne("Template.Domain.Entities.Recipe", null)
                        .WithMany("requirement")
                        .HasForeignKey("RecipeId");
                });

            modelBuilder.Entity("Template.Domain.Entities.TodoItem", b =>
                {
                    b.HasOne("Template.Domain.Entities.TodoList", "List")
                        .WithMany("Items")
                        .HasForeignKey("ListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("List");
                });

            modelBuilder.Entity("Template.Domain.Entities.TodoList", b =>
                {
                    b.OwnsOne("Template.Domain.ValueObjects.Colour", "Colour", b1 =>
                        {
                            b1.Property<Guid>("TodoListId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Code")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("TodoListId");

                            b1.ToTable("TodoLists");

                            b1.WithOwner()
                                .HasForeignKey("TodoListId");
                        });

                    b.Navigation("Colour")
                        .IsRequired();
                });

            modelBuilder.Entity("Template.Domain.Entities.User", b =>
                {
                    b.HasOne("Template.Infrastructure.Identity.IdentityUser", null)
                        .WithOne()
                        .HasForeignKey("Template.Domain.Entities.User", "IdentityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Template.Infrastructure.OAuth2.RefreshToken", b =>
                {
                    b.HasOne("Template.Infrastructure.OAuth2.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Template.Infrastructure.Identity.IdentityUser", "Identity")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("IdentityId");

                    b.Navigation("Client");

                    b.Navigation("Identity");
                });

            modelBuilder.Entity("Template.Domain.Entities.Product", b =>
                {
                    b.Navigation("allAllergies");
                });

            modelBuilder.Entity("Template.Domain.Entities.Recipe", b =>
                {
                    b.Navigation("cookingStep");

                    b.Navigation("requirement");
                });

            modelBuilder.Entity("Template.Domain.Entities.TodoList", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Template.Domain.Entities.User", b =>
                {
                    b.Navigation("allDietaryPreferences");

                    b.Navigation("favouriteRecipes");
                });

            modelBuilder.Entity("Template.Infrastructure.Identity.IdentityUser", b =>
                {
                    b.Navigation("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
