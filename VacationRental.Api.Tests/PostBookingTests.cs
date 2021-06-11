using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using VacationRental.Application.Bookings;
using VacationRental.Application.Rentals;
using Xunit;

namespace VacationRental.Api.Tests
{
    [Collection("Integration")]
    public class PostBookingTests
    {
        private readonly HttpClient _client;

        public PostBookingTests(IntegrationFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task GivenCompleteRequest_WhenPostBooking_ThenAGetReturnsTheCreatedBooking()
        {
            var postRentalRequest = new CreateRentalRequest
            {
                Units = 4
            };

            var rentalId = 0;
            using (var postRentalResponse = await _client.PostAsJsonAsync($"/api/v1/rentals", postRentalRequest))
            {
                Assert.True(postRentalResponse.IsSuccessStatusCode);
                Assert.NotNull(postRentalResponse.Headers.Location);
                rentalId = int.Parse(postRentalResponse.Headers.Location.ToString().Split('/').Last());
                Assert.True(rentalId > 0);
            }

            var postBookingRequest = new CreateBookingRequest
            {
                 RentalId = rentalId,
                 Nights = 3,
                 Start = new DateTime(2001, 01, 01)
            };

            var bookingId = 0;
            using (var postBookingResponse = await _client.PostAsJsonAsync($"/api/v1/bookings", postBookingRequest))
            {
                Assert.True(postBookingResponse.IsSuccessStatusCode);
                Assert.NotNull(postBookingResponse.Headers.Location);
                bookingId = int.Parse(postBookingResponse.Headers.Location.ToString().Split('/').Last());
                Assert.True(bookingId > 0);
            }

            using (var getBookingResponse = await _client.GetAsync($"/api/v1/bookings/{bookingId}"))
            {
                Assert.True(getBookingResponse.IsSuccessStatusCode);

                var getBookingResult = await getBookingResponse.Content.ReadAsAsync<BookingDto>();
                Assert.Equal(postBookingRequest.RentalId, getBookingResult.RentalId);
                Assert.Equal(postBookingRequest.Nights, getBookingResult.Nights);
                Assert.Equal(postBookingRequest.Start, getBookingResult.Start);
            }
        }

        [Fact]
        public async Task GivenCompleteRequest_WhenPostBooking_ThenAPostReturnsErrorWhenThereIsOverbooking()
        {
            var postRentalRequest = new CreateRentalRequest
            {
                Units = 1
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
                Nights = 3,
                Start = new DateTime(2002, 01, 01)
            };

            using (var postBooking1Response = await _client.PostAsJsonAsync($"/api/v1/bookings", postBooking1Request))
            {
                Assert.True(postBooking1Response.IsSuccessStatusCode);
            }

            var postBooking2Request = new CreateBookingRequest
            {
                RentalId = rentalId,
                Nights = 1,
                Start = new DateTime(2002, 01, 02)
            };
            
            using (var postBooking2Response = await _client.PostAsJsonAsync($"/api/v1/bookings", postBooking2Request))
            {
                Assert.Equal(HttpStatusCode.UnprocessableEntity, postBooking2Response.StatusCode);
            }
        }
    }
}
