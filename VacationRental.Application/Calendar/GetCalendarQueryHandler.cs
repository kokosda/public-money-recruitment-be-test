using System.Collections.Generic;
using System.Threading.Tasks;
using VacationRental.Application.Handlers;
using VacationRental.Core.Interfaces;
using VacationRental.Core.ResponseContainers;
using VacationRental.Domain.Bookings;
using VacationRental.Domain.Rentals;

namespace VacationRental.Application.Calendar
{
    public sealed class GetCalendarQueryHandler : GenericQueryHandlerBase<GetCalendarRequest, CalendarDto>
    {
        private readonly IGenericRepository<Rental, int> _rentalRepository;
        private readonly IBookingRepository _bookingRepository;

        public GetCalendarQueryHandler(
            IGenericRepository<Rental, int> rentalRepository,
            IBookingRepository bookingRepository)
        {
            _rentalRepository = rentalRepository;
            _bookingRepository = bookingRepository;
        }

        protected override async Task<IResponseContainerWithValue<CalendarDto>> GetResultAsync(GetCalendarRequest query)
        {
            var result = new ResponseContainerWithValue<CalendarDto>();
            var rental = await _rentalRepository.GetAsync(query.RentalId);
            
            if (rental == null)
            {
                result.AddErrorMessage("Rental not found");
                return result;
            }

            var calendarDto = new CalendarDto 
            {
                RentalId = rental.Id,
                Dates = new List<CalendarDateDto>() 
            };

            var bookings = _bookingRepository.GetBookingsByRentalId(rental.Id);
            
            for (var i = 0; i < query.Nights; i++)
            {
                var date = new CalendarDateDto
                {
                    Date = query.Start.Date.AddDays(i),
                    Bookings = new List<CalendarBookingDto>()
                };

                foreach (var booking in bookings)
                {
                    if (booking.StartDate <= date.Date && booking.StartDate.AddDays(booking.Nights) > date.Date)
                    {
                        date.Bookings.Add(new CalendarBookingDto { Id = booking.Id });
                    }
                }

                calendarDto.Dates.Add(date);
            }

            result.SetSuccessValue(calendarDto);
            return result;
        }
    }
}