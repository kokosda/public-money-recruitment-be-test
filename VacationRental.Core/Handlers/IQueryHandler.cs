using System.Threading.Tasks;
using VacationRental.Core.Interfaces;

namespace VacationRental.Core.Handlers
{
    public interface IQueryHandler<in T>
    {
        Task<IResponseContainer> HandleAsync(T query);
    }
}