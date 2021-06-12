using System;
using VacationRental.Core.Interfaces;
using VacationRental.Core.ResponseContainers;

namespace VacationRental.Domain.Bookings
{
    public class BookingIntersectsWithDateRangeSpecification : ISpecification<Booking, int>
    {
        private readonly DateTime _endDate;
        private readonly DateTime _startDate;

        public BookingIntersectsWithDateRangeSpecification(DateTime startDate, int nights)
        {
            _startDate = startDate;
            _endDate = startDate.AddDays(nights);
        }

        public IResponseContainer IsSatisfiedBy(Booking booking)
        {
            var result = new ResponseContainer();

            if (booking.EndDate < _startDate || booking.StartDate > _endDate)
            {
                result.AddErrorMessage($"Booking {booking} does not intersect with date range [{_startDate} - {_endDate}].");
                return result;
            }

            return result;
        }
    }
}