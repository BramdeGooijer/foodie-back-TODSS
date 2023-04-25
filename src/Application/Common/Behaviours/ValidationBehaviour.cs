using FluentValidation.Results;
using ValidationException = Template.Application.Common.Exceptions.ValidationException;

namespace Template.Application.Common.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
	private readonly IRequestService _requestService;
	private readonly IEnumerable<IValidator<TRequest>> _validators;

	public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators, IRequestService requestService)
	{
		_validators = validators;
		_requestService = requestService;
	}

	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		if (_validators.Any())
		{
			ValidationContext<TRequest> context = new(request);

			ValidationResult[] validationResults =
				await Task.WhenAll(_validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

			List<ValidationFailure> failures = validationResults
				.Where(validationResult => validationResult.Errors.Any())
				.SelectMany(validationResult => validationResult.Errors)
				.ToList();

			if (failures.Any())
			{
				throw new ValidationException(failures, _requestService.AcceptLanguage is not null);
			}
		}

		return await next();
	}
}
