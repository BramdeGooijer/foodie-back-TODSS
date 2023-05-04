using System.Reflection;

namespace Template.Application.Common.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
	}

	private void ApplyMappingsFromAssembly(Assembly assembly)
	{
		Type? mapFromType = typeof(IMapFrom<>);

		const string mappingMethodName = nameof(IMapFrom<object>.Mapping);

		bool HasInterface(Type t)
		{
			return t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;
		}

		List<Type> types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

		Type[] argumentTypes = { typeof(Profile) };

		foreach (Type type in types)
		{
			var instance = Activator.CreateInstance(type);

			MethodInfo? methodInfo = type.GetMethod(mappingMethodName);

			if (methodInfo != null)
			{
				methodInfo.Invoke(instance, new object[] { this });
			}
			else
			{
				List<Type> interfaces = type.GetInterfaces().Where(HasInterface).ToList();

				if (interfaces.Count > 0)
				{
					foreach (MethodInfo? interfaceMethodInfo in interfaces.Select(
						         @interface => @interface.GetMethod(mappingMethodName, argumentTypes)))
					{
						interfaceMethodInfo?.Invoke(instance, new object[] { this });
					}
				}
			}
		}
	}
}
