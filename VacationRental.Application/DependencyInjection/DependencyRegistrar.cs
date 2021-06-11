using Microsoft.Extensions.DependencyInjection;
using VacationRental.Application.Bookings;
using VacationRental.Application.Calendar;
using VacationRental.Application.Rentals;
using VacationRental.Core.Handlers;
using VacationRental.Domain.Bookings;

namespace VacationRental.Application.DependencyInjection
{
	public static class DependencyRegistrar
	{
		public static IServiceCollection AddApplicationLevelServices(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddSingleton<IGenericQueryHandler<GetRentalRequest, RentalDto>, GetRentalQueryHandler>();
			serviceCollection.AddSingleton<IGenericQueryHandler<GetBookingRequest, BookingDto>, GetBookingQueryHandler>();
			serviceCollection.AddSingleton<IGenericQueryHandler<GetCalendarRequest, CalendarDto>, GetCalendarQueryHandler>();
			serviceCollection.AddSingleton<IGenericCommandHandler<CreateRentalRequest, RentalDto>, CreateRentalCommandHandler>();
			serviceCollection.AddSingleton<IGenericCommandHandler<CreateBookingRequest, BookingDto>, CreateBookingCommandHandler>();
			serviceCollection.AddSingleton<IBookingFactory, BookingFactory>();
			return serviceCollection;
		}
	}
}
