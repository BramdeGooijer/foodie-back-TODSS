﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Template.Application.Common.Exceptions;

namespace Template.Presentation.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
	private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

	public ApiExceptionFilterAttribute()
	{
		// Register known exception types and handlers.
		_exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
		{
			{ typeof(ValidationException), HandleValidationException },
			{ typeof(NotFoundException), HandleNotFoundException },
			{ typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
			{ typeof(ForbiddenAccessException), HandleForbiddenAccessException }
		};
	}

	public override void OnException(ExceptionContext context)
	{
		HandleException(context);

		base.OnException(context);
	}

	private void HandleException(ExceptionContext context)
	{
		Type type = context.Exception.GetType();
		if (_exceptionHandlers.ContainsKey(type))
		{
			_exceptionHandlers[type].Invoke(context);
			return;
		}

		if (!context.ModelState.IsValid)
		{
			HandleInvalidModelStateException(context);
		}
	}

	private static void HandleValidationException(ExceptionContext context)
	{
		ValidationException exception = (ValidationException)context.Exception;

		ValidationProblemDetails details = new ValidationProblemDetails(exception.Errors)
		{
			Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
		};

		context.Result = new BadRequestObjectResult(details);

		context.ExceptionHandled = true;
	}

	private static void HandleInvalidModelStateException(ExceptionContext context)
	{
		ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState)
		{
			Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
		};

		context.Result = new BadRequestObjectResult(details);

		context.ExceptionHandled = true;
	}

	private static void HandleNotFoundException(ExceptionContext context)
	{
		NotFoundException exception = (NotFoundException)context.Exception;

		ProblemDetails details = new ProblemDetails
		{
			Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
			Title = "The specified resource was not found.",
			Detail = exception.Message
		};

		context.Result = new NotFoundObjectResult(details);

		context.ExceptionHandled = true;
	}

	private static void HandleUnauthorizedAccessException(ExceptionContext context)
	{
		ProblemDetails details = new ProblemDetails
		{
			Status = StatusCodes.Status401Unauthorized,
			Title = "Unauthorized",
			Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
		};

		context.Result = new ObjectResult(details)
		{
			StatusCode = StatusCodes.Status401Unauthorized
		};

		context.ExceptionHandled = true;
	}

	private static void HandleForbiddenAccessException(ExceptionContext context)
	{
		ProblemDetails details = new ProblemDetails
		{
			Status = StatusCodes.Status403Forbidden,
			Title = "Forbidden",
			Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
		};

		context.Result = new ObjectResult(details)
		{
			StatusCode = StatusCodes.Status403Forbidden
		};

		context.ExceptionHandled = true;
	}
}
