using System;
using System.Threading.Tasks;
using VacationRental.Core.Interfaces;
using VacationRental.Core.ResponseContainers;
using VacationRental.Domain.Rentals;

namespace VacationRental.Domain.Bookings
{
    public sealed class BookingFactory : IBookingFactory
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingFactory(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        
        public async Task<IResponseContainerWithValue<Booking>> CreateBooking(Rental rental, DateTime startDate, int nights)
        {
            if (rental == null)
                throw new ArgumentNullException(nameof(rental));

            var result = new ResponseContainerWithValue<Booking>();
            var createBookingSpecification = new CreateBookingSpecification(startDate.Date, nights);
            var bookings = await _bookingRepository.GetBookingsByRentalIdAsync(rental.Id);
            
            for (var i = 0; i < nights; i++)
            {
                var count = 0;
                
                foreach (var booking in bookings)
                {
                    if (createBookingSpecification.IsSatisfiedBy(booking).Result.IsSuccess)
                        count++;
                }
                
                if (count >= rental.Units)
                {
                    result.AddErrorMessage($"Booking is not available for given start date {startDate} and {nights} night(s)");
                    return result;
                }
            }
            
            var newBooking = new Booking
            {
                Nights = nights,
                RentalId = rental.Id,
                StartDate = startDate.Date
            };

            result.SetSuccessValue(newBooking);
            return result;
        }
    }
}