using System.Threading.Tasks;
using VacationRental.Core.Domain;

namespace VacationRental.Core.Interfaces
{
	public interface IGenericRepository<T, TId> where T: EntityBase<TId>
	{
		Task<T> CreateAsync(T entity);
		Task<T> GetAsync(TId id);
		Task UpdateAsync(T entity);
		Task DeleteAsync(TId id);
	}
}
