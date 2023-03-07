using Template.Application;
using Template.Infrastructure;
using Template.Presentation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPresentationServices(builder.Configuration, builder.Environment);

var app = builder.Build();

app.InitialiseAndSeedDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseHsts();
}

if (app.Configuration.GetValue<bool>("Swagger"))
{
	app.UseSwagger();
	app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
}

string[] supportedCultures = { "en" };

app.UseRequestLocalization(options =>
{
	options.SetDefaultCulture(supportedCultures.First());
	options.AddSupportedCultures(supportedCultures);
	options.AddSupportedUICultures(supportedCultures);
});

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/Health");
app.MapControllers();
app.MapRazorPages();
app.MapFallbackToPage("/NotFound");

app.Run();

// Make the implicit Program class public so test projects can access it
namespace Template.Presentation
{
	public partial class Program { }
}
