using System;
using VacationRental.Domain.Bookings;
using VacationRental.Domain.Calendars;
using Xunit;

namespace VacationRental.Domain.Tests
{
    [Collection("Common")]
    public sealed class CalendarDateIsWithinBookingDateRangeSpecificationTests
    {
        [Fact]
        public void CalendarDateIsWithinBookingDateRangeSpecification_BelongsToStartDate_ReturnsTrue()
        {
            // Arrange
            var booking = new Booking { StartDate = DateTime.UtcNow.Date, Nights = 2 };
            var calendarDate = new CalendarDate { Date = DateTime.UtcNow.Date };
            var spec = new CalendarDateIsWithinBookingDateRangeSpecification(calendarDate);

            // Act
            var result = spec.IsSatisfiedBy(booking).IsSuccess;

            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void CalendarDateIsWithinBookingDateRangeSpecification_InTheMiddle_ReturnsTrue()
        {
            // Arrange
            var booking = new Booking { StartDate = DateTime.UtcNow.Date, Nights = 2 };
            var calendarDate = new CalendarDate { Date = DateTime.UtcNow.Date.AddDays(1) };
            var spec = new CalendarDateIsWithinBookingDateRangeSpecification(calendarDate);

            // Act
            var result = spec.IsSatisfiedBy(booking).IsSuccess;

            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void CalendarDateIsWithinBookingDateRangeSpecification_BelongsToEndDate_ReturnsTrue()
        {
            // Arrange
            var booking = new Booking { StartDate = DateTime.UtcNow.Date, Nights = 2 };
            var calendarDate = new CalendarDate { Date = DateTime.UtcNow.Date.AddDays(2) };
            var spec = new CalendarDateIsWithinBookingDateRangeSpecification(calendarDate);

            // Act
            var result = spec.IsSatisfiedBy(booking).IsSuccess;

            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void CalendarDateIsWithinBookingDateRangeSpecification_IsBeforeStartDate_ReturnsFalse()
        {
            // Arrange
            var booking = new Booking { StartDate = DateTime.UtcNow.Date.AddDays(1), Nights = 3 };
            var calendarDate = new CalendarDate { Date = DateTime.UtcNow.Date };
            var spec = new CalendarDateIsWithinBookingDateRangeSpecification(calendarDate);

            // Act
            var result = spec.IsSatisfiedBy(booking).IsSuccess;

            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void CalendarDateIsWithinBookingDateRangeSpecification_IsAfterEndDate_ReturnsFalse()
        {
            // Arrange
            var booking = new Booking { StartDate = DateTime.UtcNow.Date, Nights = 3 };
            var calendarDate = new CalendarDate { Date = DateTime.UtcNow.Date.AddDays(4) };
            var spec = new CalendarDateIsWithinBookingDateRangeSpecification(calendarDate);

            // Act
            var result = spec.IsSatisfiedBy(booking).IsSuccess;

            // Assert
            Assert.False(result);
        }
    }
}