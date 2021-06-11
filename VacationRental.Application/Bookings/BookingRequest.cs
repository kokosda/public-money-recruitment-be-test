using System;
using System.ComponentModel.DataAnnotations;

namespace VacationRental.Application.Bookings
{
    public sealed class BookingRequest
    {
        private DateTime _startIgnoreTime;
        
        public int RentalId { get; set; }

        public DateTime Start
        {
            get => _startIgnoreTime;
            set => _startIgnoreTime = value.Date;
        }

        [Range(1, int.MaxValue)]
        public int Nights { get; set; }
    }
}
