using VacationRental.Core.Interfaces;
using System.Threading.Tasks;

namespace VacationRental.Core.Handlers
{
	public interface IGenericQueryHandler<in TQuery, TResult>
	{
		Task<IResponseContainerWithValue<TResult>> HandleAsync(TQuery query);
	}
}
