using VacationRental.Core.Domain;

namespace VacationRental.Domain.Rentals
{
    public sealed class Rental : EntityBase<int>
    {
        public int Units { get; set; }
    }
}