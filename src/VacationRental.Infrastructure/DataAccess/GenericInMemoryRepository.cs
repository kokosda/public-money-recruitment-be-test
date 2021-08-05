using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using VacationRental.Core.Domain;
using VacationRental.Core.Interfaces;

namespace VacationRental.Infrastructure.DataAccess
{
	public class GenericInMemoryRepository<T, TId> : IGenericRepository<T, TId> where T: EntityBase<TId>
	{
		protected const string EntityPrefix = "Entity";
		private const string ServiceEntityPrefix = "ServiceEntity";
		protected readonly IMemoryCache MemoryCache;
		protected readonly List<string> Keys = new List<string>();
		private readonly object _createSyncRoot = new object();

		public GenericInMemoryRepository(IMemoryCache memoryCache)
		{
			MemoryCache = memoryCache;
		}
		
		public Task<T> CreateAsync(T entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));
			
			lock (_createSyncRoot)
			{
				TId id;
				var lastKey = GetKey<T, string>(ServiceEntityPrefix,"Last");
				var last = MemoryCache.Get<T>(lastKey);

				if (last != null)
					id = GetNextId(last.Id);
				else
					id = GetNextId(default(TId));

				entity.Id = id;
				MemoryCache.Set(lastKey, entity);
				
				var key = GetKey<T, TId>(EntityPrefix, id);
				MemoryCache.Set(key, entity);
				Keys.Add(key);
			}
			
			var result = Task.FromResult(entity);
			return result;
		}

		public Task<T> GetAsync(TId id)
		{
			var key = GetKey<T, TId>(EntityPrefix, id);
			var result = Task.FromResult(MemoryCache.Get<T>(key));
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

		private string GetKey<TEntity, TId2>(string prefix, TId2 id)
		{
			var result = $"{prefix}.{typeof(TEntity).FullName}.{id}";
			return result;
		}

		private TId GetNextId(dynamic id)
		{
			if (id is int)
				return id + 1;

			throw new NotSupportedException($"Id of type {id.GetType()} is not yet supported.");
		}
	}
}
