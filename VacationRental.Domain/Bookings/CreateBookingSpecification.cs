using System;
using System.Threading.Tasks;
using VacationRental.Core.Interfaces;
using VacationRental.Core.ResponseContainers;

namespace VacationRental.Domain.Bookings
{
    public class CreateBookingSpecification : ISpecification<Booking, int>
    {
        private readonly DateTime _startDate;
        private readonly int _nights;

        public CreateBookingSpecification(DateTime startDate, int nights)
        {
            _startDate = startDate;
            _nights = nights;
        }
        
        public Task<IResponseContainer> IsSatisfiedBy(Booking booking)
        {
            IResponseContainer result = new ResponseContainer();

            if (booking.StartDate <= _startDate && booking.StartDate.AddDays(booking.Nights) > _startDate)
                return Task.FromResult(result);
            
            if (booking.StartDate < _startDate.AddDays(_nights) && booking.StartDate.AddDays(booking.Nights) >= _startDate.AddDays(_nights))
                return Task.FromResult(result);
                
            if (booking.StartDate > _startDate && booking.StartDate.AddDays(booking.Nights) < _startDate.AddDays(_nights))
                return Task.FromResult(result);
            
            result.AddErrorMessage($"Booking {booking} is not suitable on start date {_startDate} for the period of {_nights}.");
            return Task.FromResult(result);
        }
    }
}