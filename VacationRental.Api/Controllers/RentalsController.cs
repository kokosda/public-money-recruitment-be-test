using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Models;
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

        public RentalsController(IDictionary<int, RentalDto> rentals, IGenericQueryHandler<GetRentalRequest, RentalDto> getRentalQueryHandler)
        {
            _rentals = rentals;
            _getRentalQueryHandler = getRentalQueryHandler;
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
        public ResourceIdDto Post(PostRentalRequest model)
        {
            var key = new ResourceIdDto { Id = _rentals.Keys.Count + 1 };

            _rentals.Add(key.Id, new RentalDto
            {
                Id = key.Id,
                Units = model.Units
            });

            return key;
        }
    }
}
