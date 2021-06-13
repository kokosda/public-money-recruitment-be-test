using System.Collections.Generic;
using System.Threading.Tasks;
using VacationRental.Core.Interfaces;

namespace VacationRental.Domain.Bookings
{
    public interface IBookingRepository : IGenericRepository<Booking, int>
    {
        Task<List<Booking>> GetBookingsByRentalIdAsync(int rentalId);
    }
}