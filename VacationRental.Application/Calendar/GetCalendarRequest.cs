using System;
using System.ComponentModel.DataAnnotations;

namespace VacationRental.Application.Calendar
{
    public sealed class GetCalendarRequest
    {
        [Range(1, Double.MaxValue)]
        public int RentalId { get; set; }

        public DateTime Start { get; set; }

        [Range(0, Int32.MaxValue)]
        public int Nights { get; set; }
    }
}