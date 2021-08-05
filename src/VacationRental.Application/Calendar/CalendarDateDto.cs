using System;
using System.Collections.Generic;
using VacationRental.Domain.Calendars;

namespace VacationRental.Application.Calendar
{
    public class CalendarDateDto
    {
        public DateTime Date { get; set; }
        public List<CalendarBookingDto> Bookings { get; set; }
        public List<PreparationTimeDto> PreparationTimes { get; set; }

        public CalendarDateDto()
        {
            Bookings = new List<CalendarBookingDto>();
            PreparationTimes = new List<PreparationTimeDto>();
        }
    }
}
