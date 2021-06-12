using System;
using VacationRental.Domain.Bookings;
using VacationRental.Domain.Rentals;
using Xunit;

namespace VacationRental.Domain.Tests
{
    [Collection("Common")]
    public sealed class BookingIntersectsWithDateRangeSpecificationTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void BookingIntersectsWithDateRangeSpecification_IsWithinRange_Intersected(int preparationTimeInDays)
        {
            // Arrange
            var rental = new Rental { PreparationTimeInDays = preparationTimeInDays };
            var booking = new Booking(rental) { StartDate = DateTime.UtcNow.Date.AddDays(1), Nights = 1 };
            var spec = new BookingIntersectsWithDateRangeSpecification(DateTime.UtcNow.Date, 5);

            // Act
            var result = spec.IsSatisfiedBy(booking).IsSuccess;

            // Assert
            Assert.True(result);
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void BookingIntersectsWithDateRangeSpecification_BeforeEndDate_IntersectionPresented(int preparationTimeInDays)
        {
            // Arrange
            var rental = new Rental { PreparationTimeInDays = preparationTimeInDays };
            var booking = new Booking(rental) { StartDate = DateTime.UtcNow.Date.AddDays(3), Nights = 7 };
            var spec = new BookingIntersectsWithDateRangeSpecification(DateTime.UtcNow.Date.AddDays(1), 5);

            // Act
            var result = spec.IsSatisfiedBy(booking).IsSuccess;

            // Assert
            Assert.True(result);
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void BookingIntersectsWithDateRangeSpecification_AfterStartDate_IntersectionPresented(int preparationTimeInDays)
        {
            // Arrange
            var rental = new Rental { PreparationTimeInDays = preparationTimeInDays };
            var booking = new Booking(rental) { StartDate = DateTime.UtcNow.Date, Nights = 6 };
            var spec = new BookingIntersectsWithDateRangeSpecification(DateTime.UtcNow.Date.AddDays(3), 8);

            // Act
            var result = spec.IsSatisfiedBy(booking).IsSuccess;

            // Assert
            Assert.True(result);
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void BookingIntersectsWithDateRangeSpecification_AfterEndDate_NoIntersection(int preparationTimeInDays)
        {
            // Arrange
            var rental = new Rental { PreparationTimeInDays = preparationTimeInDays };
            var booking = new Booking(rental) { StartDate = DateTime.UtcNow.Date.AddDays(5), Nights = 2 };
            var spec = new BookingIntersectsWithDateRangeSpecification(DateTime.UtcNow.Date, 4);

            // Act
            var result = spec.IsSatisfiedBy(booking).IsSuccess;

            // Assert
            Assert.False(result);
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void BookingIntersectsWithDateRangeSpecification_BeforeStartDate_NoIntersection(int preparationTimeInDays)
        {
            // Arrange
            var rental = new Rental { PreparationTimeInDays = preparationTimeInDays };
            var booking = new Booking(rental) { StartDate = DateTime.UtcNow.Date.AddDays(-preparationTimeInDays), Nights = 2 };
            var spec = new BookingIntersectsWithDateRangeSpecification(DateTime.UtcNow.Date.AddDays(3), 7);

            // Act
            var result = spec.IsSatisfiedBy(booking).IsSuccess;

            // Assert
            Assert.False(result);
        }
    }
}