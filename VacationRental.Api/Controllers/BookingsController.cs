using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Application.Bookings;
using VacationRental.Application.Rentals;
using VacationRental.Core.Handlers;
using VacationRental.Domain.Bookings;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IGenericQueryHandler<GetBookingRequest, BookingDto> _getBookingQueryHandler;
        private readonly IGenericCommandHandler<CreateBookingRequest, BookingDto> _createBookingCommandHandler;

        public BookingsController(
            IGenericQueryHandler<GetBookingRequest, BookingDto> getBookingQueryHandler,
            IGenericCommandHandler<CreateBookingRequest, BookingDto> createBookingCommandHandler)
        {
            _getBookingQueryHandler = getBookingQueryHandler;
            _createBookingCommandHandler = createBookingCommandHandler;
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
        public async Task<ActionResult> Post(CreateBookingRequest request)
        {
            var responseContainer = await _createBookingCommandHandler.HandleAsync(request);

            if (!responseContainer.IsSuccess)
                return UnprocessableEntity(responseContainer.Messages);
            
            return Created(new Uri($"/bookins/{responseContainer.Value.Id}", UriKind.Relative), null);
        }
    }
}
