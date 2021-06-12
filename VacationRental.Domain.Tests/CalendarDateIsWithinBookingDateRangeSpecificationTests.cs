using System;
using VacationRental.Domain.Bookings;
using VacationRental.Domain.Calendars;
using VacationRental.Domain.Rentals;
using Xunit;

namespace VacationRental.Domain.Tests
{
    [Collection("Common")]
    public sealed class CalendarDateIsWithinBookingDateRangeSpecificationTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void CalendarDateIsWithinBookingDateRangeSpecification_BelongsToStartDate_ReturnsTrue(int preparationTimeInDays)
        {
            // Arrange
            var rental = new Rental { PreparationTimeInDays = preparationTimeInDays };
            var booking = new Booking(rental) { StartDate = DateTime.UtcNow.Date, Nights = 2 };
            var calendarDate = new CalendarDate { Date = DateTime.UtcNow.Date };
            var spec = new CalendarDateIsWithinBookingDateRangeSpecification(calendarDate);

            // Act
            var result = spec.IsSatisfiedBy(booking).IsSuccess;

            // Assert
            Assert.True(result);
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void CalendarDateIsWithinBookingDateRangeSpecification_InTheMiddle_ReturnsTrue(int preparationTimeInDays)
        {
            // Arrange
            var rental = new Rental { PreparationTimeInDays = preparationTimeInDays };
            var booking = new Booking(rental) { StartDate = DateTime.UtcNow.Date, Nights = 2 };
            var calendarDate = new CalendarDate { Date = DateTime.UtcNow.Date.AddDays(1) };
            var spec = new CalendarDateIsWithinBookingDateRangeSpecification(calendarDate);

            // Act
            var result = spec.IsSatisfiedBy(booking).IsSuccess;

            // Assert
            Assert.True(result);
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void CalendarDateIsWithinBookingDateRangeSpecification_BelongsToEndDate_ReturnsTrue(int preparationTimeInDays)
        {
            // Arrange
            var rental = new Rental { PreparationTimeInDays = preparationTimeInDays };
            var booking = new Booking(rental) { StartDate = DateTime.UtcNow.Date.AddDays(-preparationTimeInDays), Nights = 2 };
            var calendarDate = new CalendarDate { Date = DateTime.UtcNow.Date.AddDays(2) };
            var spec = new CalendarDateIsWithinBookingDateRangeSpecification(calendarDate);

            // Act
            var result = spec.IsSatisfiedBy(booking).IsSuccess;

            // Assert
            Assert.False(result);
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void CalendarDateIsWithinBookingDateRangeSpecification_IsBeforeStartDate_ReturnsFalse(int preparationTimeInDays)
        {
            // Arrange
            var rental = new Rental { PreparationTimeInDays = preparationTimeInDays };
            var booking = new Booking(rental) { StartDate = DateTime.UtcNow.Date.AddDays(1), Nights = 3 };
            var calendarDate = new CalendarDate { Date = DateTime.UtcNow.Date };
            var spec = new CalendarDateIsWithinBookingDateRangeSpecification(calendarDate);

            // Act
            var result = spec.IsSatisfiedBy(booking).IsSuccess;

            // Assert
            Assert.False(result);
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void CalendarDateIsWithinBookingDateRangeSpecification_IsAfterEndDate_ReturnsFalse(int preparationTimeInDays)
        {
            // Arrange
            var rental = new Rental { PreparationTimeInDays = preparationTimeInDays };
            var booking = new Booking(rental) { StartDate = DateTime.UtcNow.Date, Nights = 3 };
            var calendarDate = new CalendarDate { Date = DateTime.UtcNow.Date.AddDays(4 + preparationTimeInDays) };
            var spec = new CalendarDateIsWithinBookingDateRangeSpecification(calendarDate);

            // Act
            var result = spec.IsSatisfiedBy(booking).IsSuccess;

            // Assert
            Assert.False(result);
        }
    }
}