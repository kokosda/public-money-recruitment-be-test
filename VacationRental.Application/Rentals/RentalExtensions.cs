using System;
using VacationRental.Domain.Rentals;

namespace VacationRental.Application.Rentals
{
    public static class RentalExtensions
    {
        public static RentalDto ToDto(this Rental rental)
        {
            if (rental == null) 
                throw new ArgumentNullException(nameof(rental));
            
            var result = new RentalDto
            {
                Id = rental.Id,
                Units = rental.Units,
                PreparationTimeInDays = rental.PreparationTimeInDays
            };
            return result;
        }
    }
}