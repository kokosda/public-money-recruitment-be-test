using System.Threading.Tasks;
using VacationRental.Application.Handlers;
using VacationRental.Core.Interfaces;
using VacationRental.Core.ResponseContainers;
using VacationRental.Domain.Bookings;
using VacationRental.Domain.Rentals;

namespace VacationRental.Application.Bookings
{
    public sealed class CreateBookingCommandHandler : GenericCommandHandlerBase<CreateBookingRequest, BookingDto>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IGenericRepository<Rental, int> _rentalRepository;
        private readonly IBookingFactory _bookingFactory;

        public CreateBookingCommandHandler(
            IBookingRepository bookingRepository, 
            IGenericRepository<Rental, int> rentalRepository,
            IBookingFactory bookingFactory
        )
        {
            _bookingRepository = bookingRepository;
            _rentalRepository = rentalRepository;
            _bookingFactory = bookingFactory;
        }
        
        protected override async Task<IResponseContainerWithValue<BookingDto>> GetResultAsync(CreateBookingRequest command)
        {
            var result = new ResponseContainerWithValue<BookingDto>();
            var rental = await _rentalRepository.GetAsync(command.RentalId);
            
            if (rental == null)
            {
                result.AddErrorMessage($"{nameof(Rental)} not found.");
                return result;
            }

            var responseContainer = _bookingFactory.CreateBooking(rental, command.Start, command.Nights);

            if (!responseContainer.IsSuccess)
                result.JoinWith(responseContainer);
            else
            {
                var booking = responseContainer.Value;
                booking = await _bookingRepository.CreateAsync(booking);
                result.SetSuccessValue(booking.ToDto());
            }
            
            return result;
        }
    }
}