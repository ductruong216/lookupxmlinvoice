using LookupInvoice.Domain.Infrastructure.Abstract;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace LookupInvoice.Domain.Infrastructure.Implementation
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private COM_2300376695Entities _dbContext;
        private readonly IDbSet<T> _dbSet;
        public IDbSet<T> DbSet => _dbSet;
        protected IDbFactory DbFactory { get; set; }

        protected COM_2300376695Entities DbContext
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
            return _dbSet.ToList();
        }

        public T GetSingleById(object id)
        {
            return _dbSet.Find(id);
        }
    }
}