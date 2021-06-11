using System.Collections.Generic;
using VacationRental.Core.Domain;

namespace VacationRental.Domain.Calendars
{
    public sealed class Calendar : ValueObject
    {
        public int RentalId { get; set; }
        public List<CalendarDate> Dates { get; set; }
    }
}