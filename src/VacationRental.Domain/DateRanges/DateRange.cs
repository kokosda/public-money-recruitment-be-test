using System;
using VacationRental.Core.Domain;

namespace VacationRental.Domain.DateRanges
{
    public sealed class DateRange : ValueObject
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public DateRange(DateTime startDate, int periodInDays)
        {
            if (periodInDays <= 0)
                throw new InvalidOperationException($"Period in days must be greater than 0.");
            
            StartDate = startDate;
            EndDate = StartDate.AddDays(periodInDays);
        }

        public bool IntersectsWith(DateRange anotherDateRange)
        {
            if (anotherDateRange == null) 
                throw new ArgumentNullException(nameof(anotherDateRange));

            var result = !(anotherDateRange.EndDate < StartDate || anotherDateRange.StartDate > EndDate);
            return result;
        }

        /// <summary>
        /// Checks intersection excluding end date.
        /// </summary>
        public bool IncludesDate(DateTime date)
        {
            var result = StartDate <= date && EndDate > date;
            return result;
        }

        public override string ToString()
        {
            return $"{StartDate} - {EndDate}";
        }
    }
}