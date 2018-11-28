using System.Collections.Generic;
using System.Data.Entity;

namespace LookupInvoice.Domain.Infrastructure.Abstract
{
	public interface IRepository<T> where T : class
	{
		IDbFactory DbFactory { get; set; }
		DbSet<T> DbSet { get; }
		IList<T> GetAll();

		T GetSingleById(object id);
	}
}