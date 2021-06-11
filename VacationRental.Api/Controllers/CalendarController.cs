using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Application.Calendar;
using VacationRental.Core.Handlers;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/calendar")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly IGenericQueryHandler<GetCalendarRequest, CalendarDto> _calendarQueryHandler;

        public CalendarController(IGenericQueryHandler<GetCalendarRequest, CalendarDto> calendarQueryHandler)
        {
            _calendarQueryHandler = calendarQueryHandler;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] GetCalendarRequest request)
        {
            var responseContainer = await _calendarQueryHandler.HandleAsync(request);

            if (!responseContainer.IsSuccess)
                return UnprocessableEntity(responseContainer.Messages);

            return new JsonResult(responseContainer.Value);
        }
    }
}
