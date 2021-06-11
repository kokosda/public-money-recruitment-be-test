using System;
using System.ComponentModel.DataAnnotations;

namespace VacationRental.Application.Bookings
{
    public sealed class GetBookingRequest
    {
        [Range(1, Int32.MaxValue)]
        public int BookingId { get; set; }
    }
}