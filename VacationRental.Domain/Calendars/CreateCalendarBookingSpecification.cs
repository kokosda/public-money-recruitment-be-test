using System.Threading.Tasks;
using VacationRental.Core.Interfaces;
using VacationRental.Core.ResponseContainers;
using VacationRental.Domain.Bookings;

namespace VacationRental.Domain.Calendars
{
    public class CreateCalendarBookingSpecification : ISpecification<Booking, int>
    {
        private readonly CalendarDate _calendarDate;

        public CreateCalendarBookingSpecification(CalendarDate calendarDate)
        {
            _calendarDate = calendarDate;
        }
        
        public IResponseContainer IsSatisfiedBy(Booking booking)
        {
            var result = new ResponseContainer();
            
            if (booking.StartDate <= _calendarDate.Date && booking.StartDate.AddDays(booking.Nights) > _calendarDate.Date)
                return result;
            
            result.AddErrorMessage("Calendar date does not belong to the booking date range.");
            return result;
        }
    }
}