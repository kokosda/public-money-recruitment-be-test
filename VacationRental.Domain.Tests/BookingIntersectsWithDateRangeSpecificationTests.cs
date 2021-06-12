using System;
using VacationRental.Domain.Bookings;
using Xunit;

namespace VacationRental.Domain.Tests
{
    [Collection("Common")]
    public sealed class BookingIntersectsWithDateRangeSpecificationTests
    {
        [Fact]
        public void BookingIntersectsWithDateRangeSpecification_IsWithinRange_IntersectionPresented()
        {
            // Arrange
            var booking = new Booking { StartDate = DateTime.UtcNow.Date.AddDays(1), Nights = 1 };
            var spec = new BookingIntersectsWithDateRangeSpecification(DateTime.UtcNow.Date, 5);

            // Act
            var result = spec.IsSatisfiedBy(booking).Result.IsSuccess;

            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void BookingIntersectsWithDateRangeSpecification_BeforeEndDate_IntersectionPresented()
        {
            // Arrange
            var booking = new Booking { StartDate = DateTime.UtcNow.Date.AddDays(3), Nights = 7 };
            var spec = new BookingIntersectsWithDateRangeSpecification(DateTime.UtcNow.Date.AddDays(1), 5);

            // Act
            var result = spec.IsSatisfiedBy(booking).Result.IsSuccess;

            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void BookingIntersectsWithDateRangeSpecification_AfterStartDate_IntersectionPresented()
        {
            // Arrange
            var booking = new Booking { StartDate = DateTime.UtcNow.Date, Nights = 6 };
            var spec = new BookingIntersectsWithDateRangeSpecification(DateTime.UtcNow.Date.AddDays(3), 8);

            // Act
            var result = spec.IsSatisfiedBy(booking).Result.IsSuccess;

            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void BookingIntersectsWithDateRangeSpecification_AfterEndDate_NoIntersection()
        {
            // Arrange
            var booking = new Booking { StartDate = DateTime.UtcNow.Date.AddDays(5), Nights = 2 };
            var spec = new BookingIntersectsWithDateRangeSpecification(DateTime.UtcNow.Date, 4);

            // Act
            var result = spec.IsSatisfiedBy(booking).Result.IsSuccess;

            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void BookingIntersectsWithDateRangeSpecification_BeforeStartDate_NoIntersection()
        {
            // Arrange
            var booking = new Booking { StartDate = DateTime.UtcNow.Date, Nights = 2 };
            var spec = new BookingIntersectsWithDateRangeSpecification(DateTime.UtcNow.Date.AddDays(3), 7);

            // Act
            var result = spec.IsSatisfiedBy(booking).Result.IsSuccess;

            // Assert
            Assert.False(result);
        }
    }
}