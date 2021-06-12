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
            var startDateInUtc = startDate.ToUniversalTime().Date;
            var unitsInBookings = await GetUnitsInBookings(rental, startDateInUtc, nights);

            if (unitsInBookings >= rental.Units)
            {
                result.AddErrorMessage($"Booking is not available for given start date {startDateInUtc} and {nights} night(s)");
                return result;
            }

            var newBooking = new Booking(rental)
            {
                Nights = nights,
                StartDate = startDateInUtc,
                Unit = unitsInBookings + 1
            };

            result.SetSuccessValue(newBooking);
            return result;
        }

        private async Task<int> GetUnitsInBookings(Rental rental, DateTime startDate, int nights)
        {
            var bookings = await _bookingRepository.GetBookingsByRentalIdAsync(rental.Id);
            var bookingIntersectsWithDateRangeSpecification = new BookingIntersectsWithDateRangeSpecification(startDate, nights);
            var result = 0;

            foreach (var booking in bookings)
            {
                if (bookingIntersectsWithDateRangeSpecification.IsSatisfiedBy(booking).IsSuccess)
                    result++;
            }

            return result;
        }
    }
}