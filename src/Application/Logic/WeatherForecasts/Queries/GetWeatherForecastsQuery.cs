namespace Template.Application.Logic.WeatherForecasts.Queries;

public record GetWeatherForecastsQuery : IRequest<IEnumerable<WeatherForecastVm>>;

public class GetWeatherForecastsQueryHandler : RequestHandler<GetWeatherForecastsQuery, IEnumerable<WeatherForecastVm>>
{
	private static readonly string[] Summaries =
	{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};

	protected override IEnumerable<WeatherForecastVm> Handle(GetWeatherForecastsQuery request)
	{
		Random rng = new();

		return Enumerable.Range(1, 5).Select(index => new WeatherForecastVm
		{
			Date = DateTime.Now.AddDays(index),
			TemperatureC = rng.Next(-20, 55),
			Summary = Summaries[rng.Next(Summaries.Length)]
		});
	}
}

public record WeatherForecastVm
{
	public DateTime Date { get; set; }

	public int TemperatureC { get; set; }

	public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

	public string? Summary { get; set; }
}
