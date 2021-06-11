using System;
using VacationRental.Core.Interfaces;
using VacationRental.Domain.Rentals;

namespace VacationRental.Domain.Bookings
{
    public interface IBookingFactory
    {
        IResponseContainerWithValue<Booking> CreateBooking(Rental rental, DateTime startDate, int nights);
    }
}