using System;
using System.Threading.Tasks;
using VacationRental.Core.Interfaces;
using VacationRental.Core.ResponseContainers;

namespace VacationRental.Domain.Bookings
{
    public class BookingIntersectsWithDateRangeSpecification : ISpecification<Booking, int>
    {
        private readonly DateTime _startDate;
        private readonly DateTime _endDate;

        public BookingIntersectsWithDateRangeSpecification(DateTime startDate, int nights)
        {
            _startDate = startDate;
            _endDate = startDate.AddDays(nights);
        }
        
        public Task<IResponseContainer> IsSatisfiedBy(Booking booking)
        {
            IResponseContainer result = new ResponseContainer();
            
            if (booking.EndDate < _startDate || booking.StartDate > _endDate)
            {
                result.AddErrorMessage($"Booking {booking} does not intersect with date range [{_startDate} - {_endDate}].");
                return Task.FromResult(result);
            }
            
            return Task.FromResult(result);
        }
    }
}