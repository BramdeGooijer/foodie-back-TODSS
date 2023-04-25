using Microsoft.Extensions.DependencyInjection;

namespace Template.Application.IntegrationTests;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection Remove<TService>(this IServiceCollection services)
	{
		ServiceDescriptor? serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(TService));

		if (serviceDescriptor != null)
		{
			services.Remove(serviceDescriptor);
		}

		return services;
	}
}
