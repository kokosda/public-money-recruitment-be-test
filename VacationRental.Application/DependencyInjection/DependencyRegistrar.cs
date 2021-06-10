using Microsoft.Extensions.DependencyInjection;

namespace VacationRental.Application.DependencyInjection
{
	public static class DependencyRegistrar
	{
		public static IServiceCollection AddApplicationLevelServices(this IServiceCollection serviceCollection)
		{
			return serviceCollection;
		}
	}
}
