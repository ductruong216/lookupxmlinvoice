using LookupInvoice.Domain.Infrastructure.Abstract;
using LookupInvoice.Domain.Utility;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Web.Http.Controllers;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace LookupInvoice.Domain.Infrastructure.Implementation
{
	public interface IInvoiceRepository : IRepository<DulieuHoadon>

	{
	}

	[MultiTenantFilter]
	public class InvoiceRepository : BaseRepository<DulieuHoadon>, IInvoiceRepository
	{
		public InvoiceRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}

	public class MultiTenantFilter : ActionFilterAttribute
	{
		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			var dbContext = (Entities)ObjectFactory.GetInstance<IDbFactory>();
			var connection = ConfigurationManager.ConnectionStrings["Entities"];
			var ecsBuilder = new EntityConnectionStringBuilder(connection.ConnectionString);
			var sqlBuilder = new SqlConnectionStringBuilder(ecsBuilder.ProviderConnectionString) { InitialCatalog = "0310768095" };
			if (dbContext.Database.Connection.State == ConnectionState.Open)
				dbContext.Database.Connection.Close();
			dbContext.Database.Connection.ConnectionString = sqlBuilder.ConnectionString;
		}
	}
}