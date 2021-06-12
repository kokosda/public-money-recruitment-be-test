using VacationRental.Core.Interfaces;
using VacationRental.Core.ResponseContainers;
using VacationRental.Domain.Bookings;

namespace VacationRental.Domain.Calendars
{
    public class CalendarDateIsWithinBookingDateRangeSpecification : ISpecification<Booking, int>
    {
        private readonly CalendarDate _calendarDate;

        public CalendarDateIsWithinBookingDateRangeSpecification(CalendarDate calendarDate)
        {
            _calendarDate = calendarDate;
        }

        public IResponseContainer IsSatisfiedBy(Booking booking)
        {
            var result = new ResponseContainer();

            if (booking.StartDate <= _calendarDate.Date && booking.EndDate > _calendarDate.Date)
                return result;

            result.AddErrorMessage("Calendar date does not belong to the booking date range.");
            return result;
        }
    }
}