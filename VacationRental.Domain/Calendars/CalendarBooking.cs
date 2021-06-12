using VacationRental.Core.Domain;

namespace VacationRental.Domain.Calendars
{
    public sealed class CalendarBooking : ValueObject
    {
        /// <summary>
        /// BookingId
        /// </summary>
        public int Id { get; set; }
    }
}