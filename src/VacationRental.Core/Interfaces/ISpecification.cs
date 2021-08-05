using VacationRental.Core.Domain;

namespace VacationRental.Core.Interfaces
{
    public interface ISpecification<in T, TId> where T : EntityBase<TId>
    {
        IResponseContainer IsSatisfiedBy(T entity);
    }
}