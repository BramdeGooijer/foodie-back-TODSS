using FluentValidation.Results;

namespace Template.Application.Common.Exceptions;

public class ValidationException : Exception
{
	public ValidationException() : base("One or more validation failures have occurred.")
	{
		Errors = new Dictionary<string, string[]>();
	}

	public ValidationException(string property, string errorMessage) : this()
	{
		Errors = new Dictionary<string, string[]>
		{
			{ property, new[] { errorMessage } }
		};
	}

	public ValidationException(IEnumerable<ValidationFailure> failures, bool hasLanguage = false) : this()
	{
		Errors = failures
			.GroupBy(e => e.PropertyName, e => hasLanguage ? e.ErrorMessage : e.ErrorCode)
			.ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
	}

	public IDictionary<string, string[]> Errors { get; }
}
