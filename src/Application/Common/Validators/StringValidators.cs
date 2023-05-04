namespace Template.Application.Common.Validators;

internal static class StringValidators
{
	public static IRuleBuilderOptions<T, string?> Url<T>(this IRuleBuilder<T, string?> ruleBuilder) =>
		ruleBuilder.Must(uri => uri is null || Uri.TryCreate(uri, UriKind.Absolute, out _));
}
