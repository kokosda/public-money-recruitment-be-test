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
            
            for (var i = 0; i < nights; i++)
            {
                var calendarDate = new CalendarDate
                {
                    Date = startDateInUtc.AddDays(i),
                    CalendarBookings = new List<CalendarBooking>()
                };

                var createCalendarBookingSpecification = new CreateCalendarBookingSpecification(calendarDate);

                foreach (var booking in bookings)
                {
                    if (createCalendarBookingSpecification.IsSatisfiedBy(booking).Result.IsSuccess)
                    {
                        calendarDate.CalendarBookings.Add(new CalendarBooking { BookingId = booking.Id });
                    }
                }

                calendar.Dates.Add(calendarDate);
            }
            
            result.SetSuccessValue(calendar);
            return result;
        }
    }
}