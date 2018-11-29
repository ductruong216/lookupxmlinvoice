using LookupInvoice.Domain.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LookupInvoice.Domain.Infrastructure.Implementation
{
	public class BaseRepository<T> : IRepository<T> where T : class
	{
		private Entities _dbContext;

		protected readonly DbSet<T> _dbSet;
		public DbSet<T> DbSet => _dbSet;

		public IDbFactory DbFactory
		{
			get => _dbContext;
			set => _dbContext = (Entities)value;
		}

		public BaseRepository(IDbFactory dbFactory)
		{
			_dbContext = dbFactory as Entities;
			_dbSet = _dbContext.Set<T>();
		}

		public IList<T> GetAll()
		{
			try
			{
				return _dbSet.ToList();
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public T GetSingleById(object id)
		{
			return _dbSet.Find(id);
		}
	}
}