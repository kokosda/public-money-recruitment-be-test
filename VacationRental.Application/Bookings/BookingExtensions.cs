using System;
using VacationRental.Domain.Bookings;

namespace VacationRental.Application.Bookings
{
    public static class BookingExtensions
    {
        public static BookingDto ToDto(this Booking booking)
        {
            if (booking == null) 
                throw new ArgumentNullException(nameof(booking));
            
            var result = new BookingDto
            {
                Id = booking.Id,
                Nights = booking.Nights,
                Start = booking.StartDate,
                RentalId = booking.RentalId
            };
            return result;
        }
    }
}