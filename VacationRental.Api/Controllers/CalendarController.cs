using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Models;
using VacationRental.Application.Bookings;
using VacationRental.Application.Calendar;
using VacationRental.Application.Rentals;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/calendar")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly IDictionary<int, RentalDto> _rentals;
        private readonly IDictionary<int, BookingDto> _bookings;

        public CalendarController(
            IDictionary<int, RentalDto> rentals,
            IDictionary<int, BookingDto> bookings)
        {
            _rentals = rentals;
            _bookings = bookings;
        }

        [HttpGet]
        public CalendarDto Get(GetBookingRequest request)
        {
            if (nights < 0)
                throw new ApplicationException("Nights must be positive");
            if (!_rentals.ContainsKey(rentalId))
                throw new ApplicationException("Rental not found");

            var result = new CalendarDto 
            {
                RentalId = rentalId,
                Dates = new List<CalendarDateDto>() 
            };
            for (var i = 0; i < nights; i++)
            {
                var date = new CalendarDateDto
                {
                    Date = start.Date.AddDays(i),
                    Bookings = new List<CalendarBookingDto>()
                };

                foreach (var booking in _bookings.Values)
                {
                    if (booking.RentalId == rentalId
                        && booking.Start <= date.Date && booking.Start.AddDays(booking.Nights) > date.Date)
                    {
                        date.Bookings.Add(new CalendarBookingDto { Id = booking.Id });
                    }
                }

                result.Dates.Add(date);
            }

            return result;
        }
    }
}
