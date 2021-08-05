using VacationRental.Core.Interfaces;
using System.Threading.Tasks;

namespace VacationRental.Core.Handlers
{
	public interface ICommandHandler<in T>
	{
		Task<IResponseContainer> HandleAsync(T command);
	}
}
