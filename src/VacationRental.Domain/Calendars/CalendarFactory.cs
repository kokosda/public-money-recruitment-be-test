using System;
using System.Threading.Tasks;
using VacationRental.Core.Interfaces;
using VacationRental.Core.ResponseContainers;
using VacationRental.Domain.Bookings;
using VacationRental.Domain.Rentals;

namespace VacationRental.Domain.Calendars
{
    public sealed class CalendarFactory : ICalendarFactory
    {
        private readonly IBookingRepository _bookingRepository;

        public CalendarFactory(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<IResponseContainerWithValue<Calendar>> CreateCalendar(Rental rental, DateTime start,
            int nights)
        {
            if (rental == null)
                throw new ArgumentNullException(nameof(rental));

            var result = new ResponseContainerWithValue<Calendar>();
            var startDateInUtc = start.ToUniversalTime().Date;
            var calendar = new Calendar
            {
                RentalId = rental.Id
            };

            var bookings = await _bookingRepository.GetBookingsByRentalIdAsync(rental.Id);

            for (var night = 0; night < nights; night++)
            {
                var calendarDate = new CalendarDate
                {
                    Date = startDateInUtc.AddDays(night)
                };

                foreach (var booking in bookings)
                    if (booking.IntersectsByDurationOnDate(calendarDate.Date))
                        calendarDate.Bookings.Add(new CalendarBooking { Id = booking.Id });
                    else if (booking.IntersectsByUnitPreparationPeriodOnDate(calendarDate.Date))
                        calendarDate.PreparationTimes.Add(new PreparationTime { Unit = booking.Unit });

                calendar.Dates.Add(calendarDate);
            }

            result.SetSuccessValue(calendar);
            return result;
        }
    }
}