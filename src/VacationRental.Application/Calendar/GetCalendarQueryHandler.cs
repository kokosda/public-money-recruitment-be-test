using System.Threading.Tasks;
using VacationRental.Application.Handlers;
using VacationRental.Core.Interfaces;
using VacationRental.Core.ResponseContainers;
using VacationRental.Domain.Calendars;
using VacationRental.Domain.Rentals;

namespace VacationRental.Application.Calendar
{
    public sealed class GetCalendarQueryHandler : GenericQueryHandlerBase<GetCalendarRequest, CalendarDto>
    {
        private readonly IGenericRepository<Rental, int> _rentalRepository;
        private readonly ICalendarFactory _calendarFactory;

        public GetCalendarQueryHandler(
            IGenericRepository<Rental, int> rentalRepository,
            ICalendarFactory calendarFactory
        )
        {
            _rentalRepository = rentalRepository;
            _calendarFactory = calendarFactory;
        }

        protected override async Task<IResponseContainerWithValue<CalendarDto>> GetResultAsync(GetCalendarRequest query)
        {
            var result = new ResponseContainerWithValue<CalendarDto>();
            var rental = await _rentalRepository.GetAsync(query.RentalId);
            
            if (rental == null)
            {
                result.AddErrorMessage("Rental not found");
                return result;
            }

            var responseContainer = await _calendarFactory.CreateCalendar(rental, query.Start, query.Nights);

            if (!responseContainer.IsSuccess)
            {
                result.JoinWith(responseContainer);
                return result;
            }

            var calendarDto = responseContainer.Value.ToDto();
            result.SetSuccessValue(calendarDto);
            return result;
        }
    }
}