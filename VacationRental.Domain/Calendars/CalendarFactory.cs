using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VacationRental.Core.Interfaces;
using VacationRental.Core.ResponseContainers;
using VacationRental.Domain.Bookings;
using VacationRental.Domain.Rentals;

namespace VacationRental.Domain.Calendars
{
    public sealed class CalendarFactory : ICalendarFactory
    {
        private readonly IBookingRepository _bookingRepository;

        public CalendarFactory(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        
        public async Task<IResponseContainerWithValue<Calendar>> CreateCalendar(Rental rental, DateTime start, int nights)
        {
            if (rental == null) 
                throw new ArgumentNullException(nameof(rental));

            var result = new ResponseContainerWithValue<Calendar>();
            var startDateInUtc = start.ToUniversalTime().Date;
            var calendar = new Calendar
            {
                RentalId = rental.Id,
                Dates = new List<CalendarDate>() 
            };

            var bookings = await _bookingRepository.GetBookingsByRentalIdAsync(rental.Id);
            
            for (var night = 0; night < nights; night++)
            {
                var calendarDate = new CalendarDate
                {
                    Date = startDateInUtc.AddDays(night),
                    CalendarBookings = new List<CalendarBooking>()
                };

                var calendarDateIsWithinBookingDateRangeSpecification = new CalendarDateIsWithinBookingDateRangeSpecification(calendarDate);

                foreach (var booking in bookings)
                {
                    if (calendarDateIsWithinBookingDateRangeSpecification.IsSatisfiedBy(booking).IsSuccess)
                    {
                        calendarDate.CalendarBookings.Add(new CalendarBooking { Id = booking.Id });
                    }
                }

                calendar.Dates.Add(calendarDate);
            }
            
            result.SetSuccessValue(calendar);
            return result;
        }
    }
}