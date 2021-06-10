using System.Threading.Tasks;
using VacationRental.Application.Handlers;
using VacationRental.Core.Interfaces;
using VacationRental.Core.ResponseContainers;
using VacationRental.Domain.Rentals;

namespace VacationRental.Application.Rentals
{
    public class GetRentalQueryHandler : GenericQueryHandlerBase<GetRentalRequest, RentalDto>
    {
        private readonly IGenericRepository<Rental, int> _rentalRepository;

        public GetRentalQueryHandler(IGenericRepository<Rental, int> rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }
        protected override async Task<IResponseContainerWithValue<RentalDto>> GetResultAsync(GetRentalRequest query)
        {
            var result = new ResponseContainerWithValue<RentalDto>();
            var rental = await _rentalRepository.GetAsync(query.RentalId);

            if (rental is null)
                result.AddErrorMessage($"{nameof(Rental)} not found.");
            else
                result.SetSuccessValue(rental.ToDto());

            return result;
        }
    }
}