using System;
using System.Threading.Tasks;
using VacationRental.Core.Interfaces;
using VacationRental.Domain.Rentals;

namespace VacationRental.Domain.Calendars
{
    public interface ICalendarFactory
    {
        Task<IResponseContainerWithValue<Calendar>> CreateCalendar(Rental rental, DateTime start, int nights);
    }
}