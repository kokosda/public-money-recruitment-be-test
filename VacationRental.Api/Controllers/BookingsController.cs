using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Models;
using VacationRental.Application.Bookings;
using VacationRental.Application.Rentals;
using VacationRental.Core.Handlers;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IDictionary<int, RentalDto> _rentals;
        private readonly IDictionary<int, BookingDto> _bookings;
        private readonly IGenericQueryHandler<GetBookingRequest, BookingDto> _getBookingQueryHandler;

        public BookingsController(
            IDictionary<int, RentalDto> rentals,
            IDictionary<int, BookingDto> bookings,
            IGenericQueryHandler<GetBookingRequest, BookingDto> getBookingQueryHandler)
        {
            _rentals = rentals;
            _bookings = bookings;
            _getBookingQueryHandler = getBookingQueryHandler;
        }

        [HttpGet]
        [Route("{bookingId:int}")]
        public async Task<ActionResult> Get([FromRoute] GetBookingRequest request)
        {
            var responseContainer = await _getBookingQueryHandler.HandleAsync(request);

            if (responseContainer.Value == null)
                return NotFound(responseContainer.Messages);

            return new JsonResult(responseContainer.Value);
        }

        [HttpPost]
        public ResourceIdDto Post(CreateBookingRequest request)
        {
            if (request.Nights <= 0)
                throw new ApplicationException("Nigts must be positive");
            if (!_rentals.ContainsKey(request.RentalId))
                throw new ApplicationException("Rental not found");

            for (var i = 0; i < request.Nights; i++)
            {
                var count = 0;
                foreach (var booking in _bookings.Values)
                {
                    if (booking.RentalId == request.RentalId
                        && (booking.Start <= request.Start.Date && booking.Start.AddDays(booking.Nights) > request.Start.Date)
                        || (booking.Start < request.Start.AddDays(request.Nights) && booking.Start.AddDays(booking.Nights) >= request.Start.AddDays(request.Nights))
                        || (booking.Start > request.Start && booking.Start.AddDays(booking.Nights) < request.Start.AddDays(request.Nights)))
                    {
                        count++;
                    }
                }
                if (count >= _rentals[request.RentalId].Units)
                    throw new ApplicationException("Not available");
            }


            var key = new ResourceIdDto { Id = _bookings.Keys.Count + 1 };

            _bookings.Add(key.Id, new BookingDto
            {
                Id = key.Id,
                Nights = request.Nights,
                RentalId = request.RentalId,
                Start = request.Start.Date
            });

            return key;
        }
    }
}
