using System;
using System.ComponentModel.DataAnnotations;

namespace VacationRental.Application.Bookings
{
    public sealed class CreateBookingRequest
    {
        [Range(1, int.MaxValue)]
        public int RentalId { get; set; }
        
        public DateTime Start { get; set; }

        [Range(1, int.MaxValue)]
        public int Nights { get; set; }
    }
}
