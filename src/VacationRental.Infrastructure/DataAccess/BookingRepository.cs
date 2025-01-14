﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using VacationRental.Domain.Bookings;

namespace VacationRental.Infrastructure.DataAccess
{
    public sealed class BookingRepository : GenericInMemoryRepository<Booking, int>, IBookingRepository
    {
        public BookingRepository(IMemoryCache memoryCache) : base(memoryCache)
        {
        }

        public Task<List<Booking>> GetBookingsByRentalIdAsync(int rentalId)
        {
            var result = Keys.Where(k => k.StartsWith($"{EntityPrefix}."))
                .Select(k => MemoryCache.Get<Booking>(k))
                .Where(b => b.Rental.Id == rentalId)
                .ToList();
            return Task.FromResult(result);
        }
    }
}