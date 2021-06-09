using System.Collections.Generic;

namespace VacationRental.Api.Models
{
    public class CalendarDto
    {
        public int RentalId { get; set; }
        public List<CalendarDateDto> Dates { get; set; }
    }
}
