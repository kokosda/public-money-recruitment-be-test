using System.Threading.Tasks;
using VacationRental.Application.Handlers;
using VacationRental.Core.Interfaces;
using VacationRental.Core.ResponseContainers;
using VacationRental.Domain.Bookings;

namespace VacationRental.Application.Bookings
{
    public sealed class GetBookingQueryHandler : GenericQueryHandlerBase<GetBookingRequest, BookingDto>
    {
        private readonly IGenericRepository<Booking, int> _bookingRepository;

        public GetBookingQueryHandler(IGenericRepository<Booking, int> bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        protected override async Task<IResponseContainerWithValue<BookingDto>> GetResultAsync(GetBookingRequest query)
        {
            var result = new ResponseContainerWithValue<BookingDto>();
            var booking = await _bookingRepository.GetAsync(query.BookingId);

            if (booking is null)
                result.AddErrorMessage($"{nameof(Booking)} not found.");
            else
                result.SetSuccessValue(booking.ToDto());

            return result;
        }
    }
}