using System.Data.Entity;
using LookupInvoice.Domain.Infrastructure.Abstract;

namespace LookupInvoice.Domain.Infrastructure.Implementation
{
    public class DbFactory : DbContext, IDbFactory
	{
		public DbFactory(string connectionName)
		    : base(connectionName)
	    {
	    }

	    //public InvoiceEntities Init()
	    //{
	    //	return _dbContext ?? (_dbContext = new InvoiceEntities());
	    //}
	    public void Dispose()
	    {
		    base.Dispose();
	    }

	    public void SaveChanges()
	    {
		    base.SaveChanges();
	    }
	}
}