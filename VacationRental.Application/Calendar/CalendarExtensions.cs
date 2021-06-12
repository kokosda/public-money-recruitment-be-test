using System;
using System.Linq;
using VacationRental.Domain.Calendars;

namespace VacationRental.Application.Calendar
{
    public static class CalendarExtensions
    {
        public static CalendarDto ToDto(this Domain.Calendars.Calendar calendar)
        {
            if (calendar == null) 
                throw new ArgumentNullException(nameof(calendar));

            var result = new CalendarDto
            {
                RentalId = calendar.RentalId,
                Dates = calendar.Dates.Select(cd => cd.ToDto()).ToList()
            };
            return result;
        }

        private static CalendarDateDto ToDto(this CalendarDate calendarDate)
        {
            if (calendarDate == null)
                throw new ArgumentNullException(nameof(calendarDate));

            var result = new CalendarDateDto
            {
                Date = calendarDate.Date,
                Bookings = calendarDate.CalendarBookings.Select(cb => cb.ToDto()).ToList()
            };
            return result;
        }

        private static CalendarBookingDto ToDto(this CalendarBooking calendarBooking)
        {
            if (calendarBooking == null)
                throw new ArgumentNullException(nameof(calendarBooking));
            
            var result = new CalendarBookingDto { BookingId = calendarBooking.Id };
            return result;
        }
    }
}