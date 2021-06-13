using VacationRental.Core.Interfaces;
using System.Threading.Tasks;

namespace VacationRental.Core.Handlers
{
	public interface IGenericCommandHandler<in TCommand, TResult>
	{
		Task<IResponseContainerWithValue<TResult>> HandleAsync(TCommand command);
	}
}
