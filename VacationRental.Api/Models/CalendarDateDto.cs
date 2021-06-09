using System;
using System.Collections.Generic;

namespace VacationRental.Api.Models
{
    public class CalendarDateDto
    {
        public DateTime Date { get; set; }
        public List<CalendarBookingDto> Bookings { get; set; }
    }
}
