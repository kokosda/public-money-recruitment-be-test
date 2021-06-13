using System;
using VacationRental.Core.Domain;
using VacationRental.Domain.DateRanges;
using VacationRental.Domain.Rentals;

namespace VacationRental.Domain.Bookings
{
    public sealed class Booking : EntityBase<int>
    {
        public int Nights { get; }
        public Rental Rental { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate => StartDate.AddDays(Nights);
        public int Unit { get; set; }
        public DateRange Duration { get; }
        private DateRange UnitPreparationPeriod { get; }

        public Booking(Rental rental, DateTime startDate, int nights)
        {
            Rental = rental ?? throw new ArgumentNullException(nameof(rental));
            StartDate = startDate;
            Nights = nights;
            Duration = new DateRange(startDate, nights);
            
            if (rental.PreparationTimeInDays > 0)
                UnitPreparationPeriod = new DateRange(EndDate, rental.PreparationTimeInDays);
        }

        public bool IntersectsWith(DateRange dateRange)
        {
            if (dateRange == null)
                throw new ArgumentNullException(nameof(dateRange));

            var result = Duration.IntersectsWith(dateRange);

            if (!result && UnitPreparationPeriod != null)
                result = UnitPreparationPeriod.IntersectsWith(dateRange);
            
            return result;
        }

        public bool IntersectsByDurationOnDate(DateTime date)
        {
            var result = Duration.IncludesDate(date);
            return result;
        }

        public bool IntersectsByUnitPreparationPeriodOnDate(DateTime date)
        {
            if (UnitPreparationPeriod == null)
                return false;
            
            var result = UnitPreparationPeriod.IncludesDate(date);
            return result;
        }

        public override string ToString()
        {
            return $"{StartDate}, {nameof(Nights)} {Nights}, {nameof(UnitPreparationPeriod)} {UnitPreparationPeriod}, {nameof(Rental)} {Rental.Id}";
        }
    }
}