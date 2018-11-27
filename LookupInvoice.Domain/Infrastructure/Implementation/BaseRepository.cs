using System;
using LookupInvoice.Domain.Infrastructure.Abstract;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LookupInvoice.Domain.Infrastructure.Implementation
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private Entities _dbContext;
        private readonly IDbSet<T> _dbSet;
        public IDbSet<T> DbSet => _dbSet;
        protected IDbFactory DbFactory { get; set; }

        protected Entities DbContext
        {
            get { return _dbContext ?? (_dbContext = DbFactory.Init()); }
        }

        public BaseRepository()
        {
            DbFactory = new DbFactory();
            _dbSet = DbContext.Set<T>();
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