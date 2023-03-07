using Microsoft.AspNetCore.Mvc;
using Template.Application.Logic.WeatherForecasts.Queries;

namespace Template.Presentation.Controllers;

public class WeatherForecastController : ApiControllerBase
{
	[HttpGet]
	public async Task<IEnumerable<WeatherForecastVm>> Get()
	{
		return await Mediator.Send(new GetWeatherForecastsQuery());
	}
}
