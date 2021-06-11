using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using VacationRental.Domain.Bookings;

namespace VacationRental.Infrastructure.DataAccess
{
    public sealed class BookingRepository : GenericInMemoryRepository<Booking, int>, IBookingRepository
    {
        public BookingRepository(IMemoryCache memoryCache) : base(memoryCache)
        {
        }

        public List<Booking> GetBookingsByRentalId(int rentalId)
        {
            var result = Keys.Where(k => k.StartsWith($"{EntityPrefix}."))
                .Select(k => MemoryCache.Get<Booking>(k))
                .ToList();
            return result;
        }
    }
}