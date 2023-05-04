using System.Reflection;

namespace Template.Application.Common.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
	private readonly ICurrentUserService _currentUserService;
	private readonly IIdentityService _identityService;

	public AuthorizationBehaviour(
		ICurrentUserService currentUserService,
		IIdentityService identityService)
	{
		_currentUserService = currentUserService;
		_identityService = identityService;
	}

	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		List<AuthorizeAttribute> authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>().ToList();

		if (authorizeAttributes.Any())
		{
			// Must be authenticated user
			if (_currentUserService.UserId is null)
			{
				throw new UnauthorizedAccessException();
			}

			// Role-based authorization
			List<AuthorizeAttribute> authorizeAttributesWithRoles =
				authorizeAttributes.Where(authorizeAttribute => !string.IsNullOrWhiteSpace(authorizeAttribute.Roles)).ToList();

			if (authorizeAttributesWithRoles.Any())
			{
				var authorized = false;

				foreach (var roles in authorizeAttributesWithRoles.Select(authorizeAttribute => authorizeAttribute.Roles.Split(',')))
				{
					foreach (var role in roles)
					{
						if (await _identityService.IsInRoleAsync(_currentUserService.UserId, role.Trim()))
						{
							authorized = true;
							break;
						}
					}
				}

				// Must be a member of at least one role in roles
				if (!authorized)
				{
					throw new ForbiddenAccessException();
				}
			}

			// Policy-based authorization
			List<AuthorizeAttribute> authorizeAttributesWithPolicies =
				authorizeAttributes.Where(authorizeAttribute => !string.IsNullOrWhiteSpace(authorizeAttribute.Policy)).ToList();
			if (authorizeAttributesWithPolicies.Any())
			{
				foreach (var policy in authorizeAttributesWithPolicies.Select(authorizeAttribute => authorizeAttribute.Policy))
				{
					if (!await _identityService.AuthorizeAsync(_currentUserService.UserId, policy))
					{
						throw new ForbiddenAccessException();
					}
				}
			}
		}

		// User is authorized / authorization not required
		return await next();
	}
}
