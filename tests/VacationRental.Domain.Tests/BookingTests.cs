using System;
using VacationRental.Domain.Bookings;
using VacationRental.Domain.DateRanges;
using VacationRental.Domain.Rentals;
using Xunit;

namespace VacationRental.Domain.Tests
{
    [Collection("Common")]
    public sealed class BookingTests
    {
        [Theory]
        [InlineData(0, "2000-01-01", 1)]
        [InlineData(1, "2000-01-01", 2)]
        [InlineData(3, "2000-01-01", 2)]
        public void Constructor_InitializesPropsCorrectly_ReturnsBooking(int preparationTimeInDays, DateTime startDate,
            int nights)
        {
            // Arrange
            var rental = new Rental {Units = 1, PreparationTimeInDays = preparationTimeInDays};

            // Act
            var result = new Booking(rental, startDate, nights) {Unit = 1};

            // Assert
            Assert.Equal(startDate, result.StartDate);
            Assert.Equal(nights, result.Nights);
            Assert.Equal(1, result.Unit);
            Assert.Equal(startDate.AddDays(nights), result.EndDate);
            Assert.NotNull(result.Duration);
        }

        [Fact]
        public void Constructor_NullPassedAsRental_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new Booking(null, DateTime.Now, 1));
        }

        [Theory]
        [InlineData(0, "2000-01-01", 1, "2000-01-03", 1, false)]
        [InlineData(1, "2000-01-01", 1, "2000-01-03", 1, true)]
        [InlineData(0, "2000-01-01", 5, "2000-01-02", 2, true)]
        [InlineData(0, "2000-01-03", 2, "2000-01-01", 1, false)]
        [InlineData(0, "2000-01-01", 7, "2000-01-09", 1, false)]
        [InlineData(1, "2000-01-01", 7, "2000-01-09", 1, true)]
        public void IntersectsWith_DateRange_ReturnsExpected(
            int preparationTimeInDays, 
            DateTime startDate1, 
            int nights1, 
            DateTime startDate2, 
            int nights2,
            bool expected
        )
        {
            // Arrange
            var rental = new Rental { Units = 1, PreparationTimeInDays = preparationTimeInDays };
            var booking = new Booking(rental, startDate1, nights1);
            var dateRange = new DateRange(startDate2, nights2);
            
            // Act
            var result = booking.IntersectsWith(dateRange);
            
            // Assert
            Assert.Equal(expected, result);
        }
    }
}