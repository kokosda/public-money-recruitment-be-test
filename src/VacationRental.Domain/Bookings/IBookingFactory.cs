using System;
using System.Threading.Tasks;
using VacationRental.Core.Interfaces;
using VacationRental.Domain.Rentals;

namespace VacationRental.Domain.Bookings
{
    public interface IBookingFactory
    {
        Task<IResponseContainerWithValue<Booking>> CreateBooking(Rental rental, DateTime startDate, int nights);
    }
}