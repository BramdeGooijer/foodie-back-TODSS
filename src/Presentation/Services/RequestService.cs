using Template.Application.Common.Interfaces;

namespace Template.Presentation.Services;

public class RequestService : IRequestService
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public RequestService(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}

	public string? AcceptLanguage => _httpContextAccessor.HttpContext?.Request.Headers.AcceptLanguage;
}
