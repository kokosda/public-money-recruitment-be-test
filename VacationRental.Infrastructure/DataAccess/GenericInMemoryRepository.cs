using System;
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
		
		public Task<T> CreateAsync(T entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			TId id;
			var last = _memoryCache.Get<T>($"{typeof(T).FullName}.Last");

			if (last != null)
				id = GetNextId(last.Id);
			else
				id = GetNextId(default(TId));

			entity.Id = id;
			_memoryCache.Set($"{typeof(T).FullName}.Last", entity);
			_memoryCache.Set($"{typeof(T).FullName}.{id}", entity);
			var result = Task.FromResult(entity);
			return result;
		}

		public Task<T> GetAsync(TId id)
		{
			var result = Task.FromResult(_memoryCache.Get<T>($"{typeof(T).FullName}.{id}"));
			return result;
		}

		public Task UpdateAsync(T entity)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(TId id)
		{
			throw new System.NotImplementedException();
		}

		private TId GetNextId(dynamic id)
		{
			if (id is int)
				return id + 1;

			throw new NotSupportedException($"Id of type {id.GetType()} is not yet supported.");
		}
	}
}
