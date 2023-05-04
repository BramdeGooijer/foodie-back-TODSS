using Microsoft.Extensions.Configuration;

namespace Template.Presentation.AcceptanceTests;

public static class ConfigurationHelper
{
	private static readonly IConfiguration Configuration;

	private static string? _baseUrl;

	static ConfigurationHelper()
	{
		Configuration = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json")
			.AddEnvironmentVariables()
			.Build();
	}

	public static string GetBaseUrl()
	{
		if (_baseUrl is null)
		{
			_baseUrl = Configuration["BaseUrl"];
			_baseUrl = _baseUrl.TrimEnd('/');
		}

		return _baseUrl;
	}
}
