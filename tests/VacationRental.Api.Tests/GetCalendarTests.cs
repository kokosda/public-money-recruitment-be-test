using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VacationRental.Application.Bookings;
using VacationRental.Application.Calendar;
using VacationRental.Application.Rentals;
using Xunit;

namespace VacationRental.Api.Tests
{
    [Collection("Integration")]
    public class GetCalendarTests
    {
        private readonly HttpClient _client;

        public GetCalendarTests(IntegrationFixture fixture)
        {
            _client = fixture.Client;
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0, 0, 0)]
        [InlineData(1, 1, 1, 0, 0, 0, 0, 0)]
        [InlineData(3, 1, 2, 2, 1, 0, 0, 0)]
        public async Task GivenCompleteRequest_WhenGetCalendar_ThenAGetReturnsTheCalculatedCalendar(
            int preparationTimeInDays,
            int preparationTimesCountOnDay4,
            int preparationTimesCountOnDay5,
            int preparationTimesCountOnDay6,
            int preparationTimesCountOnDay7,
            int preparationTimesCountOnDay8,
            int preparationTimesCountOnDay9,
            int preparationTimesCountOnDay10
        )
        {
            var postRentalRequest = new CreateRentalRequest
            {
                Units = 2,
                PreparationTimeInDays = preparationTimeInDays
            };

            var rentalId = 0;
            using (var postRentalResponse = await _client.PostAsJsonAsync($"/api/v1/rentals", postRentalRequest))
            {
                Assert.True(postRentalResponse.IsSuccessStatusCode);
                Assert.NotNull(postRentalResponse.Headers.Location);
                rentalId = int.Parse(postRentalResponse.Headers.Location.ToString().Split('/').Last());
                Assert.True(rentalId > 0);
            }
            
            var postBooking1Request = new CreateBookingRequest
            {
                 RentalId = rentalId,
                 Nights = 2,
                 Start = new DateTime(2000, 01, 02)
            };
            
            var bookingId1 = 0;
            using (var postBooking1Response = await _client.PostAsJsonAsync($"/api/v1/bookings", postBooking1Request))
            {
                Assert.True(postBooking1Response.IsSuccessStatusCode);
                Assert.NotNull(postBooking1Response.Headers.Location);
                bookingId1 = int.Parse(postBooking1Response.Headers.Location.ToString().Split('/').Last());
                Assert.True(bookingId1 > 0);
            }

            var postBooking2Request = new CreateBookingRequest
            {
                RentalId = rentalId,
                Nights = 2,
                Start = new DateTime(2000, 01, 03)
            };

            var bookingId2 = 0;
            using (var postBooking2Response = await _client.PostAsJsonAsync($"/api/v1/bookings", postBooking2Request))
            {
                Assert.True(postBooking2Response.IsSuccessStatusCode);
                Assert.NotNull(postBooking2Response.Headers.Location);
                bookingId2 = int.Parse(postBooking2Response.Headers.Location.ToString().Split('/').Last());
                Assert.True(bookingId2 > 0);
            }

            using (var getCalendarResponse = await _client.GetAsync($"/api/v1/calendar?rentalId={rentalId}&start=2000-01-01&nights=10"))
            {
                Assert.True(getCalendarResponse.IsSuccessStatusCode);

                var getCalendarResult = await getCalendarResponse.Content.ReadAsAsync<CalendarDto>();
                
                Assert.Equal(rentalId, getCalendarResult.RentalId);
                Assert.Equal(10, getCalendarResult.Dates.Count);

                Assert.Equal(new DateTime(2000, 01, 01).ToUniversalTime().Date, getCalendarResult.Dates[0].Date);
                Assert.Empty(getCalendarResult.Dates[0].Bookings);
                Assert.Empty(getCalendarResult.Dates[0].PreparationTimes);
                
                Assert.Equal(new DateTime(2000, 01, 02).ToUniversalTime().Date, getCalendarResult.Dates[1].Date);
                Assert.Single(getCalendarResult.Dates[1].Bookings);
                Assert.Contains(getCalendarResult.Dates[1].Bookings, x => x.Id == bookingId1);
                Assert.Empty(getCalendarResult.Dates[1].PreparationTimes);
                
                Assert.Equal(new DateTime(2000, 01, 03).ToUniversalTime().Date, getCalendarResult.Dates[2].Date);
                Assert.Equal(2, getCalendarResult.Dates[2].Bookings.Count);
                Assert.Contains(getCalendarResult.Dates[2].Bookings, x => x.Id == bookingId1);
                Assert.Contains(getCalendarResult.Dates[2].Bookings, x => x.Id == bookingId2);
                Assert.Empty(getCalendarResult.Dates[2].PreparationTimes);

                Assert.Equal(new DateTime(2000, 01, 04).ToUniversalTime().Date, getCalendarResult.Dates[3].Date);
                Assert.Single(getCalendarResult.Dates[3].Bookings);
                Assert.Contains(getCalendarResult.Dates[3].Bookings, x => x.Id == bookingId2);
                Assert.Equal(preparationTimesCountOnDay4, getCalendarResult.Dates[3].PreparationTimes.Count);
                
                Assert.Equal(new DateTime(2000, 01, 05).ToUniversalTime().Date, getCalendarResult.Dates[4].Date);
                Assert.Empty(getCalendarResult.Dates[4].Bookings);
                Assert.Equal(preparationTimesCountOnDay5, getCalendarResult.Dates[4].PreparationTimes.Count);
                
                Assert.Equal(new DateTime(2000, 01, 06).ToUniversalTime().Date, getCalendarResult.Dates[5].Date);
                Assert.Empty(getCalendarResult.Dates[5].Bookings);
                Assert.Equal(preparationTimesCountOnDay6, getCalendarResult.Dates[5].PreparationTimes.Count);
                
                Assert.Equal(new DateTime(2000, 01, 07).ToUniversalTime().Date, getCalendarResult.Dates[6].Date);
                Assert.Empty(getCalendarResult.Dates[6].Bookings);
                Assert.Equal(preparationTimesCountOnDay7, getCalendarResult.Dates[6].PreparationTimes.Count);
                
                Assert.Equal(new DateTime(2000, 01, 08).ToUniversalTime().Date, getCalendarResult.Dates[7].Date);
                Assert.Empty(getCalendarResult.Dates[7].Bookings);
                Assert.Equal(preparationTimesCountOnDay8, getCalendarResult.Dates[7].PreparationTimes.Count);
                
                Assert.Equal(new DateTime(2000, 01, 09).ToUniversalTime().Date, getCalendarResult.Dates[8].Date);
                Assert.Empty(getCalendarResult.Dates[8].Bookings);
                Assert.Equal(preparationTimesCountOnDay9, getCalendarResult.Dates[8].PreparationTimes.Count);
                
                Assert.Equal(new DateTime(2000, 01, 10).ToUniversalTime().Date, getCalendarResult.Dates[9].Date);
                Assert.Empty(getCalendarResult.Dates[9].Bookings);
                Assert.Equal(preparationTimesCountOnDay10, getCalendarResult.Dates[9].PreparationTimes.Count);
            }
        }
    }
}
