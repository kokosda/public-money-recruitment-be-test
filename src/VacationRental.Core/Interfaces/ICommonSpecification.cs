using System.Threading.Tasks;

namespace VacationRental.Core.Interfaces
{
	public interface ICommonSpecification<in T>
	{
		Task<IResponseContainer> IsSatisfiedBy(T subject);
	}
}
