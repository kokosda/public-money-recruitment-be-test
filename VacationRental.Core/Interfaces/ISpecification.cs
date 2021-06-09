using VacationRental.Core.Domain;
using System.Threading.Tasks;

namespace VacationRental.Core.Interfaces
{
	public interface ISpecification<in T, TId> where T: EntityBase<TId>
	{
		Task<IResponseContainer> IsSatisfiedBy(T entity);
	}
}
