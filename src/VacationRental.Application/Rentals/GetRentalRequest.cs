using System;
using System.ComponentModel.DataAnnotations;

namespace VacationRental.Application.Rentals
{
    public sealed class GetRentalRequest
    {
        [Range(1, Int32.MaxValue)]
        public int RentalId { get; set; }
    }
}