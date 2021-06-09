using System;
using VacationRental.Core.Domain;

namespace VacationRental.Domain.Bookings
{
    public sealed class Booking : EntityBase<int>
    {
        public int Nights { get; set; }
        public int RentalId { get; set; }
        public DateTime StartDate { get; set; }
    }
}