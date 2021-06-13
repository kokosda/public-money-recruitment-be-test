using System;
using VacationRental.Domain.DateRanges;
using Xunit;
using Xunit.Sdk;

namespace VacationRental.Domain.Tests
{
    [Collection("Common")]
    public sealed class DateRangeTests
    {
        [Theory]
        [InlineData("2000-01-01", 1, "2000-01-01", 1, true)]
        [InlineData("2000-01-01", 5, "2000-01-01", 5, true)]
        [InlineData("2000-01-01", 2, "2000-01-01", 1, true)]
        [InlineData("2000-01-01", 3, "2000-01-05", 1, false)]
        [InlineData("2000-01-05", 3, "2000-01-01", 3, false)]
        public void IntersectsWith_AnotherDateRange_ReturnsExpected(DateTime startDate1, int periodInDays1, DateTime startDate2, int periodInDays2, bool intersected)
        {
            // Arrange
            var dateRange1 = new DateRange(startDate1, periodInDays1);
            var dateRange2 = new DateRange(startDate2, periodInDays2); 

            // Act
            var result = dateRange1.IntersectsWith(dateRange2);

            // Assert
            Assert.Equal(intersected, result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Constructor_CanNotInstantiateRangeWithInvalidPeriodInDays_ThrowsException(int periodInDays)
        {
            Assert.Throws<InvalidOperationException>(() => new DateRange(DateTime.UtcNow, periodInDays));
        }

        [Theory]
        [InlineData("2000-01-01", 1, "2000-01-01", true)]
        [InlineData("2000-01-01", 1, "2000-01-02", false)]
        public void IncludesDate_(DateTime startDate, int periodInDays, DateTime checkingDate, bool expected)
        {
            // Arrange
            var dateRange = new DateRange(startDate, periodInDays);

            // Act
            var result = dateRange.IncludesDate(checkingDate);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}