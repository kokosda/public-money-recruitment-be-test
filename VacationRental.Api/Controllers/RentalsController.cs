using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Models;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/rentals")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IDictionary<int, RentalDto> _rentals;

        public RentalsController(IDictionary<int, RentalDto> rentals)
        {
            _rentals = rentals;
        }

        [HttpGet]
        [Route("{rentalId:int}")]
        public RentalDto Get(int rentalId)
        {
            if (!_rentals.ContainsKey(rentalId))
                throw new ApplicationException("Rental not found");

            return _rentals[rentalId];
        }

        [HttpPost]
        public ResourceIdDto Post(RentalRequest model)
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
