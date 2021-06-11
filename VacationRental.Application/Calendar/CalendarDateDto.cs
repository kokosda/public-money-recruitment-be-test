using System;
using System.Collections.Generic;

namespace VacationRental.Application.Calendar
{
    public class CalendarDateDto
    {
        public DateTime Date { get; set; }
        public List<CalendarBookingDto> Bookings { get; set; }
    }
}
