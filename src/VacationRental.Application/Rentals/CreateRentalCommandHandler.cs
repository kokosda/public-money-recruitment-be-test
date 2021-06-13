using System.Threading.Tasks;
using VacationRental.Application.Handlers;
using VacationRental.Core.Interfaces;
using VacationRental.Core.ResponseContainers;
using VacationRental.Domain.Rentals;

namespace VacationRental.Application.Rentals
{
    public sealed class CreateRentalCommandHandler : GenericCommandHandlerBase<CreateRentalRequest, RentalDto>
    {
        private readonly IGenericRepository<Rental, int> _rentalRepository;

        public CreateRentalCommandHandler(IGenericRepository<Rental, int> rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }
        
        protected override async Task<IResponseContainerWithValue<RentalDto>> GetResultAsync(CreateRentalRequest command)
        {
            var result = new ResponseContainerWithValue<RentalDto>();
            var rental = new Rental
            {
                Units = command.Units, 
                PreparationTimeInDays = command.PreparationTimeInDays
            };
            
            rental = await _rentalRepository.CreateAsync(rental);
            result.SetSuccessValue(rental.ToDto());
            return result;
        }
    }
}