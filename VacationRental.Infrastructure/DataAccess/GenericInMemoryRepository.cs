using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using VacationRental.Core.Domain;
using VacationRental.Core.Interfaces;

namespace VacationRental.Infrastructure.DataAccess
{
	public sealed class GenericInMemoryRepository<T, TId> : IGenericRepository<T, TId> where T: EntityBase<TId>
	{
		private readonly IMemoryCache _memoryCache;

		public GenericInMemoryRepository(IMemoryCache memoryCache)
		{
			_memoryCache = memoryCache;
		}
		
		public Task<T> CreateAsync(EntityBase<TId> entity)
		{
			throw new System.NotImplementedException();
		}

		public Task<T> GetAsync(TId id)
		{
			var result = Task.FromResult(_memoryCache.Get<T>($"{nameof(T)}.{id}"));
			return result;
		}

		public Task UpdateAsync(EntityBase<TId> entity)
		{
			throw new System.NotImplementedException();
		}

		public Task DeleteAsync(TId id)
		{
			throw new System.NotImplementedException();
		}
	}
}
