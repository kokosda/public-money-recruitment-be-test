using Microsoft.Extensions.DependencyInjection;
using VacationRental.Core.Interfaces;
using VacationRental.Infrastructure.DataAccess;

namespace VacationRental.Infrastructure.DependencyInjection
{
	public static class DependencyRegistrar
	{
		public static IServiceCollection AddInfrastructureLevelServices(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddSingleton(typeof(IGenericRepository<,>), typeof(GenericInMemoryRepository<,>));
			return serviceCollection;
		}
	}
}
