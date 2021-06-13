using System;
using System.Collections.Generic;
using VacationRental.Core.Domain;

namespace VacationRental.Domain.Calendars
{
    public sealed class CalendarDate : ValueObject
    {
        public DateTime Date { get; set; }
        public List<CalendarBooking> Bookings { get; }
        public List<PreparationTime> PreparationTimes { get; }

        public CalendarDate()
        {
            Bookings = new List<CalendarBooking>();
            PreparationTimes = new List<PreparationTime>();
        }
    }
}