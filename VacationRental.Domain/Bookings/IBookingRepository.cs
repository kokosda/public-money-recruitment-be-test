using System.Collections.Generic;
using VacationRental.Core.Interfaces;

namespace VacationRental.Domain.Bookings
{
    public interface IBookingRepository : IGenericRepository<Booking, int>
    {
        List<Booking> GetBookingsByRentalId(int rentalId);
    }
}