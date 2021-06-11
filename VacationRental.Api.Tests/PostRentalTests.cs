using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VacationRental.Application.Rentals;
using Xunit;

namespace VacationRental.Api.Tests
{
    [Collection("Integration")]
    public class PostRentalTests
    {
        private readonly HttpClient _client;

        public PostRentalTests(IntegrationFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task GivenCompleteRequest_WhenPostRental_ThenAGetReturnsTheCreatedRental()
        {
            var request = new CreateRentalRequest
            {
                Units = 25
            };

            var rentalId = 0;
            using (var postResponse = await _client.PostAsJsonAsync($"/api/v1/rentals", request))
            {
                Assert.True(postResponse.IsSuccessStatusCode);
                Assert.NotNull(postResponse.Headers.Location);
                rentalId = int.Parse(postResponse.Headers.Location.ToString().Split('/').Last());
                Assert.True(rentalId > 0);
            }

            using (var getResponse = await _client.GetAsync($"/api/v1/rentals/{rentalId}"))
            {
                Assert.True(getResponse.IsSuccessStatusCode);

                var getResult = await getResponse.Content.ReadAsAsync<RentalDto>();
                Assert.Equal(request.Units, getResult.Units);
            }
        }
    }
}
