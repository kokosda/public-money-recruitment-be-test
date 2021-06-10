using VacationRental.Core.Handlers;
using VacationRental.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace VacationRental.Application.Handlers
{
	public abstract class QueryHandlerBase<T> : IQueryHandler<T>
	{
		public async Task<IResponseContainer> HandleAsync(T query)
		{
			if (query is null)
				throw new ArgumentNullException(nameof(query));

			var result = await GetResultAsync(query);
			return result;
		}

		protected abstract Task<IResponseContainer> GetResultAsync(T command);
	}
}
