using System;
using System.ComponentModel.DataAnnotations;

namespace VacationRental.Application.Rentals
{
    public sealed class CreateRentalRequest
    {
        [Range(1, int.MaxValue)]
        public int Units { get; set; }
        
        [Range(0, Int32.MaxValue)]
        public int PreparationTimeInDays { get; set; }
    }
}
