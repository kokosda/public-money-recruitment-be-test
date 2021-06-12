using System;
using VacationRental.Core.Domain;
using VacationRental.Domain.Rentals;

namespace VacationRental.Domain.Bookings
{
    public sealed class Booking : EntityBase<int>
    {
        public int Nights { get; set; }
        public Rental Rental { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate => StartDate.AddDays(Nights);

        public override string ToString()
        {
            return $"{StartDate}, {nameof(Nights)} {Nights}, {nameof(Rental)} {Rental.Id}";
        }
    }
}