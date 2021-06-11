using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Application.Rentals;
using VacationRental.Core.Handlers;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/rentals")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IDictionary<int, RentalDto> _rentals;
        private readonly IGenericQueryHandler<GetRentalRequest, RentalDto> _getRentalQueryHandler;
        private readonly IGenericCommandHandler<CreateRentalRequest, RentalDto> _createRentalCommandHandler;

        public RentalsController(IDictionary<int, RentalDto> rentals, 
            IGenericQueryHandler<GetRentalRequest, RentalDto> getRentalQueryHandler,
            IGenericCommandHandler<CreateRentalRequest, RentalDto> createRentalCommandHandler)
        {
            _rentals = rentals;
            _getRentalQueryHandler = getRentalQueryHandler;
            _createRentalCommandHandler = createRentalCommandHandler;
        }

        [HttpGet]
        [Route("{rentalId:int}")]
        public async Task<ActionResult> Get([FromRoute] GetRentalRequest request)
        {
            var responseContainer = await _getRentalQueryHandler.HandleAsync(request);

            if (responseContainer.Value is null)
                return NotFound(responseContainer.Messages);
            
            return new JsonResult(responseContainer.Value);
        }

        [HttpPost]
        public async Task<ActionResult> Post(CreateRentalRequest model)
        {
            var responseContainer = await _createRentalCommandHandler.HandleAsync(model);

            if (!responseContainer.IsSuccess)
                return StatusCode((int)HttpStatusCode.UnprocessableEntity, responseContainer.Messages);

            return Created(new Uri($"/{responseContainer.Value.Id}", UriKind.Relative), null);
        }
    }
}
