using Microsoft.Extensions.DependencyInjection;
using VacationRental.Application.Rentals;
using VacationRental.Core.Handlers;

namespace VacationRental.Application.DependencyInjection
{
	public static class DependencyRegistrar
	{
		public static IServiceCollection AddApplicationLevelServices(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddSingleton<IGenericQueryHandler<GetRentalRequest, RentalDto>, GetRentalQueryHandler>();
			return serviceCollection;
		}
	}
}
